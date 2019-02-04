using UnityEngine;

public class SampleCondition : Condition {
    internal protected SampleCondition(ConditionType type) : base(type)
    {
    }

    public override bool OnScheduleCheck()
    {
        int result = Random.Range(0, 2);
        return result == 1;
    }
}
