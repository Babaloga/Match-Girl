using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Interactable))]
[CanEditMultipleObjects]
public class InteractableEditor : EditorWithSubEditors<ConditionCollectionEditor, ConditionCollection>
{
    private Interactable interactable;
    private SerializedProperty nameProperty;
    //private SerializedProperty interactionLocationProperty;
    private SerializedProperty RadiusProperty;
    private SerializedProperty collectionsProperty;
    private SerializedProperty defaultReactionCollectionProperty;
    private SerializedProperty characterArchetypeProperty;


    private const float collectionButtonWidth = 125f;
    private const string interactablePropNameName = "interactionName";
    //private const string interactablePropInteractionLocationName = "interactionLocation";
    private const string interactableRadiusName = "radius";
    private const string interactablePropConditionCollectionsName = "conditionCollections";
    private const string interactablePropDefaultReactionCollectionName = "defaultReactionCollection";
    private const string interactablePropCharacterArchetypeName = "characterArchetype";


    private void OnEnable ()
    {
        interactable = (Interactable)target;

        nameProperty = serializedObject.FindProperty(interactablePropNameName);
        collectionsProperty = serializedObject.FindProperty(interactablePropConditionCollectionsName);
        //interactionLocationProperty = serializedObject.FindProperty(interactablePropInteractionLocationName);
        RadiusProperty = serializedObject.FindProperty(interactableRadiusName);
        defaultReactionCollectionProperty = serializedObject.FindProperty(interactablePropDefaultReactionCollectionName);
        characterArchetypeProperty = serializedObject.FindProperty(interactablePropCharacterArchetypeName);

        CheckAndCreateSubEditors(interactable.conditionCollections);
    }


    private void OnDisable ()
    {
        CleanupEditors ();
    }


    protected override void SubEditorSetup(ConditionCollectionEditor editor)
    {
        editor.collectionsProperty = collectionsProperty;
    }


    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();
        
        CheckAndCreateSubEditors(interactable.conditionCollections);

        EditorGUILayout.PropertyField(nameProperty);
        //EditorGUILayout.PropertyField (interactionLocationProperty);
        EditorGUILayout.PropertyField(RadiusProperty);

        EditorGUILayout.PropertyField(characterArchetypeProperty);

        for (int i = 0; i < subEditors.Length; i++)
        {
            subEditors[i].OnInspectorGUI ();
            EditorGUILayout.Space ();
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace ();
        if (GUILayout.Button("Add Collection", GUILayout.Width(collectionButtonWidth)))
        {
            ConditionCollection newCollection = ConditionCollectionEditor.CreateConditionCollection ();
            collectionsProperty.AddToObjectArray (newCollection);
        }
        EditorGUILayout.EndHorizontal ();

        EditorGUILayout.Space ();

        EditorGUILayout.PropertyField (defaultReactionCollectionProperty);

        serializedObject.ApplyModifiedProperties ();
    }
}
