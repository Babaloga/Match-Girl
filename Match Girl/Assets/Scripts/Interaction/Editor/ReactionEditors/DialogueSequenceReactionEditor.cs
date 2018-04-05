using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(DialogueSequenceReaction))]
[CanEditMultipleObjects]
public class DialogueSequenceReactionEditor : ReactionEditor {

    protected override string GetFoldoutLabel()
    {
        return "Dialogue Sequence Reaction";
    }
}
