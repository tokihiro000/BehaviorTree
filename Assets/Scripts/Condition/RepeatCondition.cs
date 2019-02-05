public class RepeatCondition : Condition
{
    public bool RepeatFlg { get; set; }
    public int LoopCount { get; set; }
    private int currentLoopCount;

    internal protected RepeatCondition(ConditionType type) : base(type)
    {
        RepeatFlg = false;
        LoopCount = 0;
    }

    public override void Activate()
    {
        base.Activate();
        currentLoopCount = 0;
    }

    public override bool OnScheduleCheck()
    {
        if (RepeatFlg)
        {
            return true;
        }

        if (currentLoopCount < LoopCount)
        {
            currentLoopCount += 1;
            return true;
        }

        return false;
    }
}
