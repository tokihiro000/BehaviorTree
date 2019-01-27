using System;

interface INode {
    IDisposable Subscribe(IObserver<NodeState> observer);
    NodeState GetNodeState();
    void SetNodeState(NodeState state);
    void Init(Int64 id);
    void Activate();
    void Deactivate();
}
