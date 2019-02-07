using UnityEngine;
using System;
using System.Collections.Generic;

public class DecoratorFactory : IFactory<Decorator, DecoratorType>
{
    private static int decoratorIdSeed = 1;
    private Dictionary<Int64, Decorator> decoratorDict;

    public DecoratorFactory()
    {
        decoratorDict = new Dictionary<Int64, Decorator>();
    }

    public T Create<T>(DecoratorType type)
        where T : Decorator
    {
        var c = (T)Create(type);
        return c;
    }

    public Decorator Create(DecoratorType type)
    {
        Decorator decorator = null;
        switch (type)
        {
            case DecoratorType.Not:
                decorator = new NotDecorator();
                break;
            case DecoratorType.False:
                decorator = new FalseDecorator();
                break;
            case DecoratorType.True:
                decorator = new TrueDecorator();
                break;
            default:
                Debug.Assert(false, "未定義のデコレータータイプ");
                break;
        }

        if (decorator != null)
        {
            decorator.Init(decoratorIdSeed);
            Register(decoratorIdSeed, decorator);
            decoratorIdSeed += 1;
        }

        return decorator;
    }

    public bool Validate(Int64 id, Decorator decorator)
    {
        Debug.Assert(decoratorDict.ContainsKey(id), "指定したdecoratorがDecoratorFactoryに登録されていません");
        return decoratorDict.ContainsKey(id);
    }

    public void Register(Int64 id, Decorator decorator)
    {
        Debug.Assert(!decoratorDict.ContainsKey(id), "decoratorId: " + id + " のdecoratorはすでに登録されています");
        decoratorDict.Add(id, decorator);
    }
}