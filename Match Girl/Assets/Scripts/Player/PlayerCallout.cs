using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class PlayerCallout : MonoBehaviour {

    SphereCollider cast;

    public float callRadius = 15;
    public float callSpeed = 1;

    public static Queue<NPCInteraction> npcQueue;

    int n = 0;

	void Start () {
        npcQueue = new Queue<NPCInteraction>();
        cast = GetComponent<SphereCollider>();
        cast.radius = 0;
        cast.isTrigger = true;
        cast.enabled = false;
        gameObject.layer = 13;
        transform.localScale = new Vector3(1 / transform.parent.localScale.x, 1 / transform.parent.localScale.y, 1 / transform.parent.localScale.z);
	}

    private void Update()
    {
        //print(npcQueue.Count);
        if(npcQueue.Count > 0 && !DialogueReader.reader.showingDialogue)
        {
            if (n > 3)
            {
                NPCInteraction currentNpc = npcQueue.Dequeue();
                currentNpc.SpeakToPlayer();
                n = 0;
            }
            else
            {
                n++;
            }
        }
        else
        {
            n = 0;
        }
    }

    public void Callout()
    {
        StopAllCoroutines();
        StartCoroutine(CalloutRoutine());
    }

    IEnumerator CalloutRoutine()
    {
        cast.enabled = true;
        while (cast.radius < callRadius)
        {
            cast.radius += callSpeed * Time.deltaTime;
            yield return null;
        }
        cast.radius = 0;
        cast.enabled = false;
    }
}
