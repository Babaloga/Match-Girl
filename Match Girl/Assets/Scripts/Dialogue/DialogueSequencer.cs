using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequencer : MonoBehaviour {

    public Dialogue[] dialogues;

    private int i = 0;

    public void Interact()
    {
        DialogueReader.reader.StartDialogue(dialogues[i]);

        if (i + 1 < dialogues.Length) i++;
    }
}
