using System;

public class TrueDecorator : Decorator
{
    internal protected TrueDecorator(DecoratorType type) : base(type)
    {
    }

    protected sealed override Func<ExecuteResult, ExecuteResult> MakeDecoraterFunc1()
    {
        return (ExecuteResult r) => {
            return new ExecuteResult(ExecuteResultState.Success);
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
