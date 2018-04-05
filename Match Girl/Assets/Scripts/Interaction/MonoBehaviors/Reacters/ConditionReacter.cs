using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionReacter : MonoBehaviour
{
    public Condition condition;
    public ReactionCollection satisfiedReaction;
    public ReactionCollection unsatisfiedReaction;
    public bool callContinuous = false;

    bool alreadyCalled = false;
    bool unsatCalled = false;

    private void Update()
    {
        if (condition.satisfied)
        {
            if (callContinuous || !alreadyCalled)
            {
                //print(condition.name + " satisfied");
                if (satisfiedReaction) satisfiedReaction.React();
                alreadyCalled = true;
                unsatCalled = false;
            }
        }
        else
        {
            if (callContinuous || !unsatCalled)
            {
                //print(condition.name + " unsatisfied");
                if(unsatisfiedReaction) unsatisfiedReaction.React();
                unsatCalled = true;
                alreadyCalled = false;
            }
        }
    }
}
