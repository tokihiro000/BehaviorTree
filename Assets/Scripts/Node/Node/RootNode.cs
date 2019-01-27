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

    public override void AddNode(Node node)
    {
        Debug.Assert(childNode == null, "ID: " + this.Id + " のノードにはすでに子ノードが追加されています");
        childNode = node;
        childNode.SetOwner(this);
        this.nodeDisposable = childNode.Subscribe(this);
    }

    public override Node GetChildNode()
    {
        Debug.Assert(childNode != null, "ID: " + this.Id + " は子ノードを持っていません");
        return base.GetChildNode();
    }

    public override Node GetOwner()
    {
        Debug.Assert(false, "RootNodeはownerを持てません");
        return null;
    }

    public override void SetOwner(Node owner)
    {
        Debug.Assert(false, "RootNodeはownerを持てません");
    }

    public override bool IsRoot()
    {
        return true;
    }
}
