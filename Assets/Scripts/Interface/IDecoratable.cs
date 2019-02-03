using System;

public interface IDecoratable
{
    void Init(Int64 id);
    Int64 GetId();
    void MakeDecorater(INode child, INode owner);
    ExecuteResult EvaluateCall(ExecuteResult executeResult);
}
