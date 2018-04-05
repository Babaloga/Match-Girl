using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSequenceReaction : Reaction
{
    public DialogueSequencer sequencer;

    protected override void ImmediateReaction()
    {
        sequencer.Interact();
    }
}
