using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(Entry))]
public class EntryEditor : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect textRect = new Rect(position.x, position.y, 3 * (position.width/4), position.height);
        Rect endRect = new Rect(position.x +  3 * (position.width / 4), position.y, position.width/8, position.height);
        Rect boolRect = new Rect(position.x + 7 * (position.width / 8), position.y, position.width / 8, position.height);

        EditorGUI.PropertyField(boolRect, property.FindPropertyRelative("end"), GUIContent.none);
        EditorGUI.PropertyField(textRect, property.FindPropertyRelative("entryText"), GUIContent.none);
        EditorGUI.LabelField(endRect, new GUIContent("End:"));
    }

}
