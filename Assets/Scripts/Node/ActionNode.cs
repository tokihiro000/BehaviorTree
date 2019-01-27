using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node, IActionNode, IObserver<ActionState>
{
    // コンストラクタ
    protected ActionNode(NodeType type, NodeState state) : base(type, state) {}

    // 実行するアクション
    private IActionable action;
    public IActionable Action
    {
        get
        {
            return this.action;
        }
        set
        {
            this.action = value;
        }
    }

    // ノードの状態通知用observable
    private NodeObservable nodeObservable;

    public void Execute() {
        if (this.action == null) {
            Debug.Assert(false, "ノードID: " + this.Id + " is not assigned action");
            SetNodeState(NodeState.Disable);
            return;
        }

        if (this.nodeObservable == null) {
            Debug.Assert(false, "ノードID: " + this.Id + " has null nodeObservable");
            SetNodeState(NodeState.Disable);
            return;
        }

        action.Subscribe(this);
        action.Invoke();
    }

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    public override NodeState OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnCompleted()
    {
        Debug.Log("ActionNode OnCompleted");
    }

    public void OnError(Exception error)
    {
        Debug.Log("ActionNode OnError: " + error.Message);
    }

    public void OnNext(ActionState value)
    {
        Debug.Log("Action Node OnNext: " + value);
    }

    public IDisposable Subscribe(IObserver<NodeState> observer)
    {
        nodeObservable = new NodeObservable();
        return nodeObservable.Subscribe(observer);
    }
}
