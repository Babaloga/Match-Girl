using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCInteractionBasic : MonoBehaviour {

    Collider coll;
    WordSource source;
    public float cooldown = 5f;
    float triggerTime =-5f;
    Vector3 playerPosMemory;

    bool wantsMatches;
    bool listening = true;

    public float yesProbabilityOutOf100 = 1;
    public float negativeResponseRate = 0.1f;

    public float maxWaitTime = 40f;
    public float minWaitTime = 15f;
    private float waitTime;
    private float timeMarker;

    public GameObject noReaction;
    public GameObject yesReaction;

    NavMeshAgent agent;

    public bool dialogueShown = false;

    //Replace with static variable somewhere else
    public float priceForMatch = 2f;

    public int maxBuyNumber = 10;
    public int minBuyNumber = 1;

    private Vector3 destination;

    public NPCType npcType;

    private enum NPCState
    {
        Wandering,
        GoingToPlayer,
        WaitingForPlayer
    }

    private NPCState currentState = NPCState.Wandering;

    void Start () {
        agent = GetComponent<NavMeshAgent>();
        coll = GetComponent<Collider>();
        source = GetComponent<WordSource>();
        float random = Random.Range(0f, 100f);

        wantsMatches = (random <= yesProbabilityOutOf100);

        //if (wantsMatches)
        //{
        //    print("wantsMatches");
        //}
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

                destination = agent.destination;

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
                    //agent.SetDestination(transform.position);
                    MakeTransaction();
                }
                break;

            case NPCState.WaitingForPlayer:

                if(Time.time - timeMarker > waitTime)
                {
                    ReturnToWander();
                }

                if ((transform.position - PlayerMovement.player.transform.position).magnitude <= 2f)
                {
                    MakeTransaction();
                }

                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (listening && other.gameObject.layer == 13 && Time.time - cooldown >= triggerTime)
        {
            print("Callout Recieved");
            StartCoroutine(CalloutResponse());
        }
    }

    private IEnumerator CalloutResponse()
    {
        if(wantsMatches)
        {
            if(GetComponentInChildren<NPCAnimation>()) GetComponentInChildren<NPCAnimation>().Beckon();
            WaitForPlayer();
            source.speakPrefab = yesReaction;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

            source.Speak();
        }
        else if (Random.Range(0f,1f) < negativeResponseRate)
        {
            source.speakPrefab = noReaction;
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));

            source.Speak();
        }
    }

    private void WaitForPlayer()
    {
        listening = true;
        GetComponent<MoveTo>().overriden = true;
        agent.SetDestination(transform.position);

        waitTime = Random.Range(minWaitTime, maxWaitTime);
        timeMarker = Time.time;

        currentState = NPCState.WaitingForPlayer;
    }

    private void GoToPlayer()
    {
        StartCoroutine(GoToPlayerRoutine());
    }

    IEnumerator GoToPlayerRoutine()
    {
        while (!GetComponentInChildren<NPCAnimation>().IsBeckoning())
        {
            yield return null;
        }
        while (GetComponentInChildren<NPCAnimation>().IsBeckoning())
        {
            yield return null;
        }

        GetComponent<MoveTo>().overriden = true;
        agent.isStopped = false;
        playerPosMemory = PlayerMovement.player.transform.position;
        agent.SetDestination(playerPosMemory);
        currentState = NPCState.GoingToPlayer;
    }

    private void MakeTransaction()
    {
        int matchesWanted = Random.Range(minBuyNumber, maxBuyNumber);

        if(PlayerStatsManager.stats.matches < matchesWanted)
        {
            matchesWanted = PlayerStatsManager.stats.matches;
        }

        PlayerStatsManager.stats.money += (int) (matchesWanted * priceForMatch);
        PlayerStatsManager.stats.matches -= matchesWanted;

        wantsMatches = false;

        ReturnToWander();
    }

    private void ReturnToWander()
    {
        GetComponent<MoveTo>().overriden = false;
        GetComponent<MoveTo>().Pause(2f);

        agent.SetDestination(destination);
        currentState = NPCState.Wandering;
    }
}

public enum NPCType
{
    Rich, Poor
}
