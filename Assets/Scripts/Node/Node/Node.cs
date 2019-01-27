using System;
using UnityEngine;
using NodeId = System.Int64;

public abstract class Node : INode
{
    /// <summary>
    /// ノードのタイプ
    /// </summary>
    private NodeType nodeType;

    /// <summary>
    /// ノードの状態
    /// </summary>
    private NodeState nodeState;

    /// <summary>
    ///  ノードID
    /// </summary>
    private NodeId id;
    public NodeId Id {
        get {
            Debug.Assert(id != 0, "this node has no id. you must be use Node.CreateNode when you create Node");
            return this.id;
        }
    }

    /// <summary>
    /// ノードの状態通知用observable
    /// </summary> 
    protected NodeObservable nodeObservable;

    protected Node(NodeType type) {
        this.nodeType = type;
    }

    public virtual NodeState GetNodeState() {
        return this.nodeState;
    }

    public virtual void SetNodeState(NodeState state) {
        this.nodeState = state;
    }

    public IDisposable Subscribe(IObserver<NodeState> observer)
    {
        nodeObservable = new NodeObservable();
        return nodeObservable.Subscribe(observer);
    }

    public void Init(Int64 id) {
        this.id = id;
        SetNodeState(NodeState.Init);
    }

    public abstract void Activate();
    public abstract void Deactivate();
    public abstract NodeState OnUpdate();
}
