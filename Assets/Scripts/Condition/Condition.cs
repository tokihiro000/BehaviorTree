using System;

public abstract class Condition : IConditionable
{
    protected ConditionType conditionType;
    public virtual ConditionType ConditionType => this.conditionType = ConditionType.None;

    private Int64 conditionId;
    private Int64 id;

    protected Condition()
    {
    }

    public void Init(Int64 id)
    {
        this.conditionId = id;
    }

    public virtual void Activate()
    {
    }

    public Int64 GetId()
    {
        return this.conditionId;
    }

    public abstract bool OnScheduleCheck();
}
