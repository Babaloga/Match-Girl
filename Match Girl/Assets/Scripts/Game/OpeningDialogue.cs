using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDialogue : MonoBehaviour {

    public Dialogue openingDialogue;

    void Update () {

        DialogueReader.reader.StartDialogue(openingDialogue);

        Destroy(this);
    }
}
