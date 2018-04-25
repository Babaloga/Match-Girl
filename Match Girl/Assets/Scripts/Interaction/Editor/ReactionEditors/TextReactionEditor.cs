using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TextReaction))]
public class TextReactionEditor : ReactionEditor
{
    protected override string GetFoldoutLabel()
    {
        return "Text Reaction";
    }
}

