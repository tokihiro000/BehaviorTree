using System;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node, IObserver<NodeState>
{
    // コンストラクタ
    internal RootNode(NodeType type) : base(type) {}

    public override void Activate()
    {
        base.Activate();
    }

    public override void StartProcess()
    {
        base.StartProcess();
    }

    public override void Run()
    {
        var nextState = HasChild() ? NodeState.WaitForChild : NodeState.Complete;
        SetNodeState(nextState);
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    public override void AddNode(INode node)
    {
        Debug.Assert(childNode == null, "ID: " + GetId() + " のノードにはすでに子ノードが追加されています");
        childNode = node;
        childNode.SetOwner(this);
        this.nodeDisposable = childNode.Subscribe(this);
    }

    public override INode GetChildNode()
    {
        Debug.Assert(childNode != null, "ID: " + GetId() + " は子ノードを持っていません");
        return base.GetChildNode();
    }

    public override INode GetOwner()
    {
        Debug.Assert(false, "RootNodeはownerを持てません");
        return null;
    }

    public override void SetOwner(INode owner)
    {
        Debug.Assert(false, "RootNodeはownerを持てません");
    }

    public sealed override bool IsRoot()
    {
        return true;
    }

    public sealed override void OnCompleted()
    {
        SetNodeState(NodeState.Complete);
        executeResult = childNode.GetExecuteResultState();
    }

    public sealed override void OnError(Exception error)
    {
        SetNodeState(NodeState.Complete);
        executeResult = new ExecuteResult(ExecuteResultState.Error);
    }
}
