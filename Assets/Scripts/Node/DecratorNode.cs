using System;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorNode : Node, IObserver<NodeState>, IDecoratorNode
{
    private IDecoratable decoratable;

    public void SetDecoratable(IDecoratable decoratable)
    {
        this.decoratable = decoratable;
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type">Type.</param>
    internal DecoratorNode(NodeType type) : base(type)
    {
    }

    public override void Activate()
    {
        Debug.Assert(decoratable != null, "decoratable がnullです @ DecoratorNode.Activate");
        decoratable?.MakeDecorater(GetChildNode(), GetOwner());
        base.Activate();
    }

    public override void StartProcess()
    {
        base.StartProcess();
    }

    public override void Run()
    {
        SetNodeState(NodeState.WaitForChild);
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
        Debug.Log("DecoratorNode OnCompleted");
        SendResult(ExecuteResultState.Success);
    }

    public override void OnError(Exception error)
    {
        Debug.Log("DecoratorNode OnError: " + error.Message);
        SendResult(ExecuteResultState.Failure, error);
    }

    public virtual ExecuteResult Decorate(ExecuteResult result)
    {
        if (decoratable != null) 
        {
            return decoratable.EvaluateCall(result);
        }

        return new ExecuteResult(result.GetExecuteResult());
    }

    private void SendResult(ExecuteResultState state, Exception error = null)
    {
        SetNodeState(NodeState.Complete);
        Debug.Assert(nodeObservable != null, "nodeObservableがnullです");

        executeResult = Decorate(new ExecuteResult(state));
        var resultState = executeResult.GetExecuteResult();
        switch (resultState) {
            case ExecuteResultState.Success:
                nodeObservable?.SendComplete();
                break;
            case ExecuteResultState.Failure:
                nodeObservable?.SendError(error);
                break;
            default:
                Debug.Assert(false, "DecratorNodeの結果が未定義です");
                break;
        }
    }
}
