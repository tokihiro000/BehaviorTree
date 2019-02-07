using System;

public interface IConditionable
{
    ConditionType ConditionType {
        get;
    }

    void Init(Int64 id);
    void Activate();
    Int64 GetId();
    bool OnScheduleCheck();
}
