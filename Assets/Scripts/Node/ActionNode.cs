using System;
using UnityEngine;

public class ActionNode : Node, IActionNode, IObserver<ActionState>
{
    // コンストラクタ
    internal ActionNode(NodeType type) : base(type) { }

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

    public void ExecuteAction()
    {
        if (this.action == null)
        {
            Debug.Assert(false, "ノードID: " + GetId() + " is not assigned action");
            SetNodeState(NodeState.Disable);
            return;
        }

        if (this.nodeObservable == null)
        {
            Debug.Assert(false, "ノードID: " + GetId() + " has null nodeObservable");
            SetNodeState(NodeState.Disable);
            return;
        }

        action.Subscribe(this);
        action.Invoke();
    }

    public override void Activate()
    {
        base.Activate();
        this.action.Activate();
    }

    public override void StartProcess()
    {
        base.StartProcess();
    }

    public override void Run()
    {
        base.Run();
        ExecuteAction();
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    public override void AddNode(INode node)
    {
        Debug.Assert(false, "ID: " + GetId() + " にはノードを追加することはできません");
    }

    void IObserver<ActionState>.OnCompleted()
    {
        Debug.Log("ActionNode OnCompleted");
        SetNodeState(NodeState.Complete);

        var actionResult = action.GetResult();
        switch (actionResult?.GetExecuteResult()) {
            case ActionResultState.Success:
                executeResult = new ExecuteResult(ExecuteResultState.Success);
                break;
            case ActionResultState.Failure:
                executeResult = new ExecuteResult(ExecuteResultState.Failure);
                break;
            case ActionResultState.None:
            default:
                Debug.Assert(false, "ActionResultが未定義の値です");
                break;
        }

        Debug.Assert(nodeObservable != null, "nodeObservableがnullです");
        nodeObservable?.SendComplete();
    }

    void IObserver<ActionState>.OnError(Exception error)
    {
        Debug.Log("ActionNode OnError: " + error.Message);
        SetNodeState(NodeState.Complete);

        executeResult = new ExecuteResult(ExecuteResultState.Error);

        Debug.Assert(nodeObservable != null, "nodeObservableがnullです");
        nodeObservable.SendError(error);
    }

    void IObserver<ActionState>.OnNext(ActionState value)
    {
        switch (value)
        {
            case ActionState.None:
                break;
            case ActionState.Start:
                break;
            case ActionState.Running:
                break;
            case ActionState.Finished:
                break;
            default:
                Debug.Assert(false, "undefined Action State in ActionNode.OnNext");
                break;
        }

        Debug.Log("Action Node OnNext: " + value);
    }

    public override bool IsRoot()
    {
        return false;
    }
}
