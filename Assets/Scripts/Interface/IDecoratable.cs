using System;

public interface IDecoratable
{
    DecoratorType DecoratorType {
        get;
    }
    void Init(Int64 id);
    void Activate();
    Int64 GetId();
    void MakeDecorater(INode child, INode owner);
    ExecuteResult EvaluateCall(ExecuteResult executeResult);
}
