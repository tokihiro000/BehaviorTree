﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class ConditionNode : Node, IObserver<NodeState>, IConditionNode
{
    public override NodeType NodeType => this.nodeType = NodeType.Condition;

    public IConditionable Condition
    {
        get;
        set;
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type">Type.</param>
    internal ConditionNode() : base()
    {
    }

    public override void Activate()
    {
        base.Activate();
        Condition.Activate();
    }

    public override void StartProcess()
    {
        base.StartProcess();
    }

    public override void Run()
    {
        Debug.Assert(this.Condition != null, "Condition が nullです");
        if (this.Condition.OnScheduleCheck())
        {
            Debug.Log("Condition Node: true");
            // 子がいるなら子へ遷移
            if (HasChild())
            {
                SetNodeState(NodeState.WaitForChild);
                return;
            }

            executeResult = new ExecuteResult(ExecuteResultState.Success);
        }
        else
        {
            Debug.Log("Condition Node: false");
            executeResult = new ExecuteResult(ExecuteResultState.Failure);
        }

        SetNodeState(NodeState.Complete);
        nodeObservable?.SendComplete();
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
        base.OnCompleted();
    }

    public override void OnError(Exception error)
    {
        Debug.Log("ConditionNode OnError: " + error.Message);
        base.OnError(error);
    }
}
