using UnityEngine;
using System;
using System.Collections.Generic;

public class ActionFactory : IFactory<Action, ActionType>
{
    private static int actionIdSeed = 1;
    private Dictionary<Int64, Action> actionDict;

    public ActionFactory()
    {
        actionDict = new Dictionary<Int64, Action>();
    }

    public T Create<T>(ActionType type)
    where T : Action
    {
        var c = (T)Create(type);
        return c;
    }

    public Action Create(ActionType type)
    {
        Action action = null;
        switch (type)
        {
            case ActionType.Sample1:
                action = new SampleAction();
                break;
            case ActionType.Sample2:
                action = new SampleAction2();
                break;
            default:
                Debug.Assert(false, "未定義のActionタイプ");
                break;
        }

        if (action != null)
        {
            action.Init(actionIdSeed);
            Register(actionIdSeed, action);
            actionIdSeed += 1;
        }

        return action;
    }

    public bool Validate(Int64 id, Action action)
    {
        Debug.Assert(actionDict.ContainsKey(id), "指定したActionがActionFactoryに登録されていません");
        return actionDict.ContainsKey(id);
    }

    public void Register(Int64 id, Action action)
    {
        Debug.Assert(!actionDict.ContainsKey(id), "actionId: " + id + " のActionはすでに登録されています");
        actionDict.Add(id, action);
    }
}