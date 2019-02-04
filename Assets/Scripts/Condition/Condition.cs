using System;

public abstract class Condition : IConditionable
{
    private Int64 conditionId;
    private ConditionType conditionType;
    private Int64 id;

    protected Condition(ConditionType type)
    {
        this.conditionType = type;
    }

    public void Init(Int64 id)
    {
        this.conditionId = id;
    }

    public Int64 GetId()
    {
        return this.conditionId;
    }

    public abstract bool OnScheduleCheck();
}
