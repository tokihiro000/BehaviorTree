using System;

public interface IConditionable
{
    void Init(Int64 id);
    Int64 GetId();
    bool OnScheduleCheck();
}
