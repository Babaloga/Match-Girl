﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionCollection : ScriptableObject {

    public string description;
    public Condition[] requiredConditions;
    public ReactionCollection reactionCollection;

	public bool CheckAndReact()
    {
        for (int i = 0; i < requiredConditions.Length; i++)
        {
            if (!AllConditions.CheckCondition(requiredConditions[i]))
            {
                return false;
            }
        }

        if (reactionCollection)
        {
            reactionCollection.React();
        }

        return true;
    }
}