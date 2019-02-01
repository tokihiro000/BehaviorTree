using System;
using System.Collections.Generic;

public interface INode {
    void Init(Int64 id);
    void Activate();
    void StartProcess();
    void Run();
    void DoNext();
    void Deactivate();

    IDisposable Subscribe(IObserver<NodeState> observer);
    NodeState GetNodeState();
    void SetNodeState(NodeState state);

    void AddNode(INode node);
    INode GetChildNode();

    void SetOwner(INode owner);
    INode GetOwner();

    ExecuteResult GetExecuteResultState();
    bool IsRoot();
    bool HasChild();
    Int64 GetId();
}