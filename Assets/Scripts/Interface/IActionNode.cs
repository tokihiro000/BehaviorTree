using System;
public interface IActionNode
{
    IActionable Action
    {
        get;
        set;
    }

    IDisposable Subscribe(IObserver<NodeState> observer);
    NodeState GetNodeState();
    void SetNodeState(NodeState state);
    void Execute();
}
