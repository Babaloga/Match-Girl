using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateSceneManager : MonoBehaviour {

    bool startTrip = false;
    bool endTrip = false;

    public Dialogue[] openingDialogues;

    private void Awake()
    {
        GetComponent<OpeningDialogue>().openingDialogue = openingDialogues[PersistentGameManager.currentDay];
    }

    private void Update()
    {
        if (!startTrip) startTrip = DialogueReader.reader.showingDialogue;
        else
        {
            if (!endTrip) endTrip = !DialogueReader.reader.showingDialogue;
            else
            {
                PersistentGameManager.instance.LoadMainScene();
                enabled = false;
            }
        }
    }
}
