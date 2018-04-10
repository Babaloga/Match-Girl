using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDialogue : MonoBehaviour {

    public Dialogue openingDialogue;

    void Update () {
        if (PersistentGameManager.currentDay == 0)
        {
            DialogueReader.reader.StartDialogue(openingDialogue);
        }

        Destroy(this);
    }
}
