using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteDepthRenderer))]
public class SpriteDepthRendererEditor : Editor{

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("This class is to be assigned at runtime only by GlobalSpriteDepth", MessageType.Warning);
    }

}
