using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ConditionReacter))]
[CanEditMultipleObjects]
public class ConditionReacterEditor : EditorWithSubEditors<ConditionCollectionEditor, ConditionCollection>
{
    private SerializedProperty collectionsProperty;
    private SerializedProperty callContinuousProperty;
    private SerializedProperty unsatisfiedReactionProperty;
    private SerializedProperty satisfiedReactionProperty;

    private const string reacterPropConditionCollectionsName = "condition";
    private const string reacterPropCallContinuousName = "callContinuous";
    private const string reacterPropUnsatisfiedReactionCollectionName = "unsatisfiedReaction";
    private const string reacterPropSatisfiedReactionCollectionName = "satisfiedReaction";


    private void OnEnable()
    {
        collectionsProperty = serializedObject.FindProperty(reacterPropConditionCollectionsName);
        callContinuousProperty = serializedObject.FindProperty(reacterPropCallContinuousName);
        unsatisfiedReactionProperty = serializedObject.FindProperty(reacterPropUnsatisfiedReactionCollectionName);
        satisfiedReactionProperty = serializedObject.FindProperty(reacterPropSatisfiedReactionCollectionName);
    }


    private void OnDisable()
    {
        CleanupEditors();
    }


    protected override void SubEditorSetup(ConditionCollectionEditor editor)
    {
        editor.collectionsProperty = collectionsProperty;
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(collectionsProperty);

        EditorGUILayout.PropertyField(callContinuousProperty);

        EditorGUILayout.PropertyField(satisfiedReactionProperty);

        EditorGUILayout.PropertyField(unsatisfiedReactionProperty);

        serializedObject.ApplyModifiedProperties();
    }
}
