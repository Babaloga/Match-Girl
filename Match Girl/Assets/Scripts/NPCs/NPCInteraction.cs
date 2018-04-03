using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCInteraction : MonoBehaviour {

    //Collider coll;
    WordSource source;
    public float cooldown = 5f;
    float triggerTime =-5f;
    Vector3 playerPosMemory;

    bool wantsMatches;
    bool listening = true;

    public float yesProbabilityOutOf100 = 1;

    public GameObject noReaction;
    public GameObject yesReaction;

    NavMeshAgent agent;

    public static Dialogue[] interactionDialogues;

    //public static Queue<NPCInteraction> talkToPlayerQueue;

    private static NPCInteraction mainNPC;

    public bool dialogueShown = false;

    private enum NPCState
    {
        Wandering,
        GoingToPlayer,
        Waiting,
        Speaking
    }

    private NPCState currentState = NPCState.Wandering;

    private void Awake()
    {
        if (!mainNPC)
        {
            mainNPC = this;

            print("mainNPC");

            interactionDialogues = Resources.LoadAll<Dialogue>("GeneralNPCDialogues");
            //talkToPlayerQueue = new Queue<NPCInteraction>();
        }
    }

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        //coll = GetComponent<Collider>();
        source = GetComponent<WordSource>();
        float random = Random.Range(0f, 100f);

        wantsMatches = (random <= yesProbabilityOutOf100);

        if (wantsMatches)
        {
            print("wantsMatches");
        }
    }

    private void Update()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        switch(currentState)
        {
            case NPCState.Wandering:
                rend.color = Color.yellow;
                listening = true;
                //agent.isStopped = false;
                GetComponent<MoveTo>().overriden = false;

                break;

            case NPCState.GoingToPlayer:
                rend.color = Color.blue;
                listening = false;
                GetComponent<MoveTo>().overriden = true;

                Vector3 playerPos = PlayerMovement.player.transform.position;

                if ((playerPos - playerPosMemory).magnitude > 1.5f)
                {
                    playerPosMemory = playerPos;
                    agent.SetDestination(playerPos);
                }

                if ((transform.position - PlayerMovement.player.transform.position).magnitude <= 2f)
                {
                    agent.isStopped = true;
                    //agent.SetDestination(transform.position);
                    EnterQueue();
                }

                break;

            case NPCState.Waiting:
                rend.color = Color.red;
                listening = false;
                GetComponent<MoveTo>().overriden = true;

                dialogueShown = false;

                break;

            case NPCState.Speaking:
                rend.color = Color.green;
                listening = false;
                GetComponent<MoveTo>().overriden = true;

                if (DialogueReader.reader.showingDialogue)
                {
                    dialogueShown = true;
                }
                else
                {
                    if(dialogueShown) EndSpeaking();
                }

                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (listening && other.gameObject.layer == 13 && Time.time - cooldown >= triggerTime)
        {
            CalloutResponse();
        }
    }

    private void CalloutResponse()
    {
        if(wantsMatches)
        {
            source.speakPrefab = yesReaction;
            source.Speak();
            GoToPlayer();
        }
        else
        {
            source.speakPrefab = noReaction;
            source.Speak();
        }
    }

    private void GoToPlayer()
    {
        GetComponent<MoveTo>().overriden = true;
        playerPosMemory = PlayerMovement.player.transform.position;
        agent.SetDestination(playerPosMemory);
        agent.isStopped = false;
        currentState = NPCState.GoingToPlayer;
    }

    private void EnterQueue()
    {
        currentState = NPCState.Waiting;

        PlayerCallout.npcQueue.Enqueue(this);
    }

    public void SpeakToPlayer()
    {
        print(interactionDialogues.Length);
        DialogueReader.reader.StartDialogue(interactionDialogues[Random.Range(0, interactionDialogues.Length)]);

        currentState = NPCState.Speaking;
    }

    private void EndSpeaking()
    {
        wantsMatches = false;
        //talkToPlayerQueue.Dequeue();

        GetComponent<MoveTo>().DestinationReached();
        GetComponent<MoveTo>().Resume();

        currentState = NPCState.Wandering;
    }
}
