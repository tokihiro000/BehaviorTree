﻿using System;

public class FalseDecorator : Decorator
{
    public sealed override DecoratorType DecoratorType => this.decoratorType = DecoratorType.False;

    protected internal FalseDecorator() : base()
    {
    }

    protected sealed override Func<ExecuteResult, ExecuteResult> MakeDecoraterFunc1()
    {
        return (ExecuteResult r) => {
            return new ExecuteResult(ExecuteResultState.Failure);
        };
    }

    protected sealed override Func<ExecuteResult, INode, ExecuteResult> MakeDecoraterFunc2()
    {
        return null;
    }

    protected sealed override Func<ExecuteResult, INode, INode, ExecuteResult> MakeDecoraterFunc3()
    {
        return null;
    }
}
