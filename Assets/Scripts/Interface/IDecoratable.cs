using System;

public interface IDecoratable
{
    void MakeDecorater(INode child, INode owner);
    ExecuteResult EvaluateCall(ExecuteResult executeResult);
}
