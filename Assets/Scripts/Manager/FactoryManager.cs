using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FactoryManager : Manager<FactoryManager>
{
    private static readonly NodeFactory nodeFactory = new NodeFactory();
    private static readonly ActionFactory actionFactory = new ActionFactory();
    private static readonly DecoratorFactory decoratorFactory = new DecoratorFactory();

    public FactoryManager()
    {
    }

    public IFactory<T, U> GetFactory<T, U>(FactoryType type)
        where T : class
        where U : struct
    {
        switch (type) {
            case FactoryType.Node:
                return (IFactory<T, U>) nodeFactory;
            case FactoryType.Action:
                return (IFactory<T, U>) actionFactory;
            case FactoryType.Decorator:
                return (IFactory<T, U>) decoratorFactory;
            default:
                Debug.Assert(false, "未定義のファクトリーです");
                break;
        }

        return null;
    }
}
