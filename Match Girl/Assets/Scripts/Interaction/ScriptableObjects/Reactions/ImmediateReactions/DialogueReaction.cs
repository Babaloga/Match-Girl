using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueReaction : Reaction {

    public Dialogue dialogue;

    private DialogueReader reader;

    protected override void SpecificInit()
    {
        reader = DialogueReader.reader;
    }


    protected override void ImmediateReaction()
    {
        reader.StartDialogue(dialogue);
    }
}
