using UnityEngine;
using System;
using System.Collections.Generic;

public class ConditionFactory : IFactory<Condition, ConditionType>
{
    private static int conditionIdSeed = 1;
    private Dictionary<Int64, Condition> conditionDict;

    public ConditionFactory()
    {
        conditionDict = new Dictionary<Int64, Condition>();
    }

    public Condition Create(ConditionType type)
    {
        Condition condition = null;
        switch (type)
        {
            case ConditionType.Sample1:
                condition = new SampleCondition(type);
                break;
            default:
                Debug.Assert(false, "未定義のコンディションタイプ");
                break;
        }

        if (condition != null)
        {
            condition.Init(conditionIdSeed);
            Register(conditionIdSeed, condition);
            conditionIdSeed += 1;
        }

        return condition;
    }

    public bool Validate(Int64 id, Condition condition)
    {
        Debug.Assert(conditionDict.ContainsKey(id), "指定したconditionがConditionFactoryに登録されていません");
        return conditionDict.ContainsKey(id);
    }

    public void Register(Int64 id, Condition condition)
    {
        Debug.Assert(!conditionDict.ContainsKey(id), "conditionId: " + id + " のconditionはすでに登録されています");
        conditionDict.Add(id, condition);
    }
}