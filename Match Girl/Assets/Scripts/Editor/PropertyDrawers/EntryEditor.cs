//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomPropertyDrawer(typeof(Entry))]
//public class EntryEditor : PropertyDrawer {

//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        float totalWidth = 0;
//        Rect textRect = new Rect(position.x, position.y, (position.width/4), position.height);
//        totalWidth += textRect.width;

//        Rect modTempLabel = new Rect(position.x + totalWidth, position.y, position.width / 16, position.height);
//        totalWidth += modTempLabel.width;
//        Rect modTempRect = new Rect(position.x + totalWidth, position.y, position.width / 16, position.height);
//        totalWidth += modTempRect.width;

//        Rect modHungerLabel = new Rect(position.x + totalWidth, position.y, position.width / 16, position.height);
//        totalWidth += modHungerLabel.width;
//        Rect modHungerRect = new Rect(position.x + totalWidth, position.y, position.width / 16, position.height);
//        totalWidth += modHungerRect.width;

//        Rect modTimeLabel = new Rect(position.x + totalWidth, position.y, position.width / 16, position.height);
//        totalWidth += modTimeLabel.width;
//        Rect modTimeRect = new Rect(position.x + totalWidth, position.y, position.width / 16, position.height);
//        totalWidth += modTimeRect.width;

//        Rect modMatchesLabel = new Rect(position.x + totalWidth, position.y, position.width / 16, position.height);
//        totalWidth += modMatchesLabel.width;
//        Rect modMatchesRect = new Rect(position.x + totalWidth, position.y, position.width / 16, position.height);
//        totalWidth += modMatchesRect.width;

//        Rect modMoneyLabel = new Rect(position.x + totalWidth, position.y, position.width / 16, position.height);
//        totalWidth += modMoneyLabel.width;
//        Rect modMoneyRect = new Rect(position.x + totalWidth, position.y, position.width / 16, position.height);
//        totalWidth += modMoneyRect.width;

//        Rect endRect = new Rect(position.x + totalWidth, position.y, position.width/16, position.height);
//        totalWidth += endRect.width;
//        Rect boolRect = new Rect(position.x + totalWidth, position.y, position.width / 16, position.height);

//        EditorGUI.PropertyField(boolRect, property.FindPropertyRelative("end"), GUIContent.none);
//        EditorGUI.PropertyField(textRect, property.FindPropertyRelative("entryText"), GUIContent.none);

//        EditorGUI.PropertyField(modTempRect, property.FindPropertyRelative("modifyTemperature"), GUIContent.none);
//        EditorGUI.PropertyField(modHungerRect, property.FindPropertyRelative("modifyHunger"), GUIContent.none);
//        EditorGUI.PropertyField(modTimeRect, property.FindPropertyRelative("modifyTime"), GUIContent.none);
//        EditorGUI.PropertyField(modMatchesRect, property.FindPropertyRelative("modifyMatches"), GUIContent.none);
//        EditorGUI.PropertyField(modMoneyRect, property.FindPropertyRelative("modifyMoney"), GUIContent.none);

//        EditorGUI.LabelField(endRect, new GUIContent("End:"));

//        EditorGUI.LabelField(modTempLabel, new GUIContent("Warmth"));
//        EditorGUI.LabelField(modHungerLabel, new GUIContent("Hunger"));
//        EditorGUI.LabelField(modTimeLabel, new GUIContent("Time"));
//        EditorGUI.LabelField(modMatchesLabel, new GUIContent("Matches"));
//        EditorGUI.LabelField(modMoneyLabel, new GUIContent("Money"));
//    }

//}
