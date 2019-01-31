using System;
using System.Collections.Generic;

interface INode {
    IDisposable Subscribe(IObserver<NodeState> observer);
    NodeState GetNodeState();
    void SetNodeState(NodeState state);
    void Init(Int64 id);
    void Activate();
    void StartProcess();
    void Run();
    void DoNext();
    void Deactivate();
    void AddNode(Node node);
    Node GetChildNode();
    void SetOwner(Node owner);
    Node GetOwner();
    ExecuteResult GetExecuteResultState();
    bool IsRoot();
    bool HasChild();
}