using System;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNode : Node, IObserver<NodeState>, IConditionNode
{
    public IConditionable Condition
    {
        get;
        set;
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type">Type.</param>
    internal ConditionNode(NodeType type) : base(type)
    {
    }

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
        Debug.Assert(this.Condition != null, "Condition が nullです");
        if (this.Condition.OnScheduleCheck()) {
            Debug.Log("Condition Node: true");
            executeResult = new ExecuteResult(ExecuteResultState.Success);
        } else {
            Debug.Log("Condition Node: false");
            executeResult = new ExecuteResult(ExecuteResultState.Failure);
        }

        this.OnCompleted();
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

    public override bool IsRoot()
    {
        return false;
    }

    public override void OnCompleted()
    {
        Debug.Log("ConditionNode OnCompleted");
        SetNodeState(NodeState.Complete);
        nodeObservable?.SendComplete();
    }

    public override void OnError(Exception error)
    {
        Debug.Log("ConditionNode OnError: " + error.Message);
        SetNodeState(NodeState.Complete);
        nodeObservable?.SendError(error);
    }
}
