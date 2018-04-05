using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaReacter : MonoBehaviour {
    public Collider2D trigger;
    public ReactionCollection enterReaction;
    public ReactionCollection exitReaction;
    public bool callContinuous = false;

    GameObject player;

    bool enterCalled = false;
    bool exitCalled = false;

    private void Start()
    {
        player = PlayerMovement.player;
        trigger = GetComponent<Collider2D>();
        trigger.isTrigger = true;
    }

    private void Update()
    {
        if (trigger.bounds.Contains(player.transform.position))
        {
            if (callContinuous || !enterCalled)
            {
                //print(condition.name + " satisfied");
                enterReaction.React();
                enterCalled = true;
                exitCalled = false;
            }
        }
        else
        {
            if (callContinuous || !exitCalled)
            {
                //print(condition.name + " unsatisfied");
                exitReaction.React();
                exitCalled = true;
                enterCalled = false;
            }
        }
    }
}
