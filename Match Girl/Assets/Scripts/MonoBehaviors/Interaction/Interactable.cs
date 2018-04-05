using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactableName = "Interactable";
    public Transform GUILocation;
    public ConditionCollection[] conditionCollections = new ConditionCollection[0];
    public ReactionCollection defaultReactionCollection;

    private void Start()
    {
        if (!GUILocation)
            GUILocation = transform;

        if (!defaultReactionCollection)
        {
            foreach(ReactionCollection r in GetComponentsInChildren<ReactionCollection>())
            {
                if(r.gameObject.name.ToLower() == "default reaction" || r.gameObject.name.ToLower() == "defaultreaction")
                {
                    defaultReactionCollection = r;

                    break;
                }
            }
        }
    }

    public void Interact ()
    {
        for (int i = 0; i < conditionCollections.Length; i++)
        {
            if (conditionCollections[i].CheckAndReact())
            {
                return;
            }
        }

        defaultReactionCollection.React ();
    }
}
