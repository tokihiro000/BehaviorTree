using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Node, IActionNode, IObserver<ActionState>
{
    // コンストラクタ
    internal ActionNode(NodeType type) : base(type) {}

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
        SetNodeState(NodeState.Complete);
        nodeObservable.SendComplete();
    }

    public void OnError(Exception error)
    {
        Debug.Log("ActionNode OnError: " + error.Message);
        nodeObservable.SendError(error);
    }

    public void OnNext(ActionState value)
    {
        switch (value) {
            case ActionState.None:
                break;
            case ActionState.Start:
                SetNodeState(NodeState.Start);
                nodeObservable.SendState(GetNodeState());
                break;
            case ActionState.Running:
                SetNodeState(NodeState.Running);
                nodeObservable.SendState(GetNodeState());
                break;
            case ActionState.Finished:
                SetNodeState(NodeState.Complete);
                nodeObservable.SendState(GetNodeState());
                break;
            default:
                Debug.Assert(false, "undefined Action State in ActionNode.OnNext");
                break;
        }

        Debug.Log("Action Node OnNext: " + value);
    }
}
