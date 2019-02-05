using System;
using System.Collections.Generic;
using UnityEngine;

public class DecoratorNode : Node, IObserver<NodeState>, IDecoratorNode
{
    public override NodeType NodeType => this.nodeType = NodeType.Decorator;

    private IDecoratable decoratable;
    public void SetDecoratable(IDecoratable decoratable)
    {
        this.decoratable = decoratable;
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type">Type.</param>
    internal DecoratorNode() : base()
    {
    }

    public override void Activate()
    {
        Debug.Assert(decoratable != null, "decoratable がnullです @ DecoratorNode.Activate");
        decoratable?.MakeDecorater(GetChildNode(), GetOwner());
        base.Activate();
        decoratable.Activate();
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
        Debug.Assert(childNode != null, "childNodeがnull");
        SendResult(childNode.GetExecuteResultState());
    }

    public override void OnError(Exception error)
    {
        Debug.Log("DecoratorNode OnError: " + error.Message);
        base.OnError(error);
    }

    public virtual ExecuteResult Decorate(ExecuteResult result)
    {
        if (decoratable != null) 
        {
            return decoratable.EvaluateCall(result);
        }

        return new ExecuteResult(result.GetExecuteResult());
    }

    private void SendResult(ExecuteResult result, Exception error = null)
    {
        SetNodeState(NodeState.Complete);
        Debug.Assert(nodeObservable != null, "nodeObservableがnullです");

        executeResult = Decorate(result);
        nodeObservable?.SendComplete();
    }
}
