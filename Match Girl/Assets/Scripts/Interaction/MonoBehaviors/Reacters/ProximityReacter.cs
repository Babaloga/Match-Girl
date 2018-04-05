using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityReacter : MonoBehaviour
{
    public float radius;
    public ReactionCollection enterReaction;
    public ReactionCollection exitReaction;
    public bool callContinuous = false;
    public bool destroyAfterStart = false;

    public Transform target;

    bool enterCalled = false;
    bool exitCalled = false;

    private void Start()
    {
        if (!target)
            target = PlayerMovement.player.transform;
    }

    private void Update()
    {
        Vector3 relativePos = target.position - transform.position;

        if (relativePos.magnitude <= radius)
        {
            if (callContinuous || !enterCalled)
            {
                if (enterReaction)
                {
                    //print(condition.name + " satisfied");
                    enterReaction.React();
                    enterCalled = true;
                    exitCalled = false;
                }
            }
        }
        else
        {
            if (callContinuous || !exitCalled)
            {
                //print(condition.name + " unsatisfied");
                if (exitReaction)
                {
                    exitReaction.React();
                    exitCalled = true;
                    enterCalled = false;
                }
            }
        }

        if (destroyAfterStart)
        {
            enabled = false;
        }
    }
}
