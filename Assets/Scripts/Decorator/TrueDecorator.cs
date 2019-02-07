using System;

public class TrueDecorator : Decorator
{
    protected internal TrueDecorator() : base()
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
