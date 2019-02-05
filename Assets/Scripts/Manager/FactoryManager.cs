using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class FactoryManager : Manager<FactoryManager>
{
    private static readonly NodeFactory nodeFactory = new NodeFactory();
    private static readonly ActionFactory actionFactory = new ActionFactory();
    private static readonly DecoratorFactory decoratorFactory = new DecoratorFactory();
    private static readonly ConditionFactory conditionFactory = new ConditionFactory();

    public FactoryManager()
    {
    }

    public X GetFactory<X> (FactoryType type)
    {
        switch (type)
        {
            case FactoryType.Node:
                return (X)GetFactory<Node, NodeType> (type);
            case FactoryType.Action:
                return (X)GetFactory<Action, ActionType> (type);
            case FactoryType.Decorator:
                return (X)GetFactory<Decorator, DecoratorType>(type);
            case FactoryType.Condition:
                return (X)GetFactory<Condition, ConditionType>(type);
            default:
                Debug.Assert(false, "未定義のファクトリーです");
                break;
        }

        return default(X);
    }

    public X GetFactory<T, U, X>(FactoryType type)
        where T : class
        where U : struct
        where X : IFactory<T, U>
    {
        var factory = (X)GetFactory<T, U>(type);
        return factory;
    }

    public IFactory<T, U> GetFactory<T, U>(FactoryType type)
        where T : class
        where U : struct
    {
        switch (type)
        {
            case FactoryType.Node:
                return (IFactory<T, U>)nodeFactory;
            case FactoryType.Action:
                return (IFactory<T, U>)actionFactory;
            case FactoryType.Decorator:
                return (IFactory<T, U>)decoratorFactory;
            case FactoryType.Condition:
                return (IFactory<T, U>)conditionFactory;
            default:
                Debug.Assert(false, "未定義のファクトリーです");
                break;
        }
        
        return null;
    }
}
