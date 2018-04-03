using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour {

    public static int selectionInt = 10;
    public DialogueReader dialogueReader;

    public void Select()
    {
        dialogueReader.Select(selectionInt);
    }
}
