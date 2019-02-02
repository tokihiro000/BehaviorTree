using System;
using UnityEngine;

public abstract class AbstractDecorator : IDecoratable
{
    INode targetChild, targetOwner;
    private Func<ExecuteResult, ExecuteResult> evalFunc1;
    private Func<ExecuteResult, INode, ExecuteResult> evalFunc2;
    private Func<ExecuteResult, INode, INode, ExecuteResult> evalFunc3;

    public void MakeDecorater(INode child = null, INode owner = null)
    {
        this.targetChild = child;
        this.targetOwner = owner;
        evalFunc1 = MakeDecoraterFunc1();
        evalFunc2 = MakeDecoraterFunc2();
        evalFunc3 = MakeDecoraterFunc3();
    }

    public ExecuteResult EvaluateCall(ExecuteResult executeResult)
    {
        if (evalFunc3 != null) 
        {
            return evalFunc3(executeResult, targetChild, targetOwner);
        } 
        else if (evalFunc2 != null)
        {
            return evalFunc2(executeResult, targetChild);
        }
        else if (evalFunc1 != null)
        {
            return evalFunc1(executeResult);
        }
        else
        {
            Debug.Assert(false, "Decoratorのコールバックが全てnullです");
            return new ExecuteResult(ExecuteResultState.Failure);
        }
    }

    protected abstract Func<ExecuteResult, ExecuteResult> MakeDecoraterFunc1();
    protected abstract Func<ExecuteResult, INode, ExecuteResult> MakeDecoraterFunc2();
    protected abstract Func<ExecuteResult, INode, INode, ExecuteResult> MakeDecoraterFunc3();
}
