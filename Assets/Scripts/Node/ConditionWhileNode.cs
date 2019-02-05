using System;
using System.Collections.Generic;
using UnityEngine;

public class ConditionWhileNode : Node, IObserver<NodeState>, IConditionNode
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
    internal ConditionWhileNode(NodeType type) : base(type)
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
        Debug.Assert(HasChild(), "ConditionWhileNodeには子が必要です");

        if (this.Condition.OnScheduleCheck())
        {
            Debug.Log("Condition While Node: true");
            SetNodeState(NodeState.WaitForChild);
            return;
        }

        Debug.Log("Condition While Node: false");
        executeResult = new ExecuteResult(ExecuteResultState.Failure);
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
        if (this.Condition.OnScheduleCheck())
        {
            Debug.Log("Condition While Node: true @OnCompleted");
            // 条件がtrueなので再びこの実行を始める
            // Warning: this.ActivateだとConditionもリセットされるのでbase.Activate
            base.Activate();
            SetNodeState(NodeState.WaitForChild);
            return;
        }

        Debug.Log("Condition While Node: false @OnCompleted");
        base.OnCompleted();
    }

    public override void OnError(Exception error)
    {
        Debug.Log("ConditionNode OnError: " + error.Message);
        base.OnError(error);
    }
}
