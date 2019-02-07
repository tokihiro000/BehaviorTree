using UnityEngine;

public class SampleCondition : Condition {
    public override ConditionType ConditionType => this.conditionType = ConditionType.Sample1;

    protected internal SampleCondition() : base()
    {
    }

    public override bool OnScheduleCheck()
    {
        int result = Random.Range(0, 2);
        return result == 1;
    }
}
