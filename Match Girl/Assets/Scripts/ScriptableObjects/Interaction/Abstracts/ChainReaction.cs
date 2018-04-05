using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class ChainReaction : Reaction
{
    public float delay = 0;

    public ReactionCollection[] reactArray;

    protected abstract bool ProceedCondition();

    protected abstract int ReactCondition();

    public new void React(MonoBehaviour monoBehaviour)
    {
        monoBehaviour.StartCoroutine(ReactCoroutine());
    }

    protected override void ImmediateReaction() { }

    protected IEnumerator ReactCoroutine()
    {
        yield return new WaitForSeconds(delay);

        ImmediateReaction();

        yield return new WaitUntil(() => ProceedCondition() == true);

        if (reactArray[ReactCondition()])
        {
            reactArray[ReactCondition()].React();
        }
    }

}
