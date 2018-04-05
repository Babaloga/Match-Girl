using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInteractionReaction : Reaction
{
    public SpecialInteraction special;

    protected override void ImmediateReaction()
    {
        special.Interact();
    }
}
