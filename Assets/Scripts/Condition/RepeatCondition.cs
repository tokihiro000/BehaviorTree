public class RepeatCondition : Condition
{
    public override ConditionType ConditionType => this.conditionType = ConditionType.Repeat;

    public bool RepeatFlg { get; set; }
    public int LoopCount { get; set; }
    private int currentLoopCount;

    protected internal RepeatCondition() : base()
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
