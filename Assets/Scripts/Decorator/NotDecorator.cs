using System;

public class NotDecorator : AbstractDecorator
{
    protected sealed override Func<ExecuteResult, ExecuteResult> MakeDecoraterFunc1()
    {
        return ((ExecuteResult r) => {
            return new ExecuteResult(
                    r.GetExecuteResult() == ExecuteResultState.Failure ? ExecuteResultState.Success : ExecuteResultState.Failure
               );
            }
       );
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
