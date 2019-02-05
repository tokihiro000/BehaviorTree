using System;

public interface IConditionable
{
    void Init(Int64 id);
    void Activate();
    Int64 GetId();
    bool OnScheduleCheck();
}
