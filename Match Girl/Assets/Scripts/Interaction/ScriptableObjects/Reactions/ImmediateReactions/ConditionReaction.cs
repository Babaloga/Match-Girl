public class ConditionReaction : DelayedReaction
{
    public Condition condition;
    public bool satisfied;


    protected override void ImmediateReaction ()
    {
        condition.satisfied = satisfied;
    }
}
