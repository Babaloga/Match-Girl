using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(SpecialInteractionReaction))]
[CanEditMultipleObjects]
public class SpecialInteractionReactionEditor : ReactionEditor {

    protected override string GetFoldoutLabel()
    {
        return "Special Interaction Reaction";
    }
}
