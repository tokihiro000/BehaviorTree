using System;
using System.Collections.Generic;
using UnityEngine;
using NodeId = System.Int64;

public abstract class Node : INode, IObserver<NodeState>
{
    /// <summary>
    /// ノードのタイプ
    /// </summary>
    private NodeType nodeType;

    /// <summary>
    /// ノードの状態
    /// </summary>
    private NodeState nodeState;

    /// <summary>
    /// 直近のcompositノード。なければnull
    /// </summary>
    private INode owner;

    protected IDisposable nodeDisposable;

    /// <summary>
    /// 接続された子ノード
    /// </summary>
    protected INode childNode;

    /// <summary>
    /// 実行後の成功可否
    /// </summary>
    protected ExecuteResult executeResult;

    /// <summary>
    ///  ノードID
    /// </summary>
    private NodeId id;
    public Int64 GetId() {
        return this.id;
    }

    /// <summary>
    /// ノードの状態通知用observable
    /// </summary> 
    protected NodeObservable nodeObservable;

    protected Node(NodeType type)
    {
        this.nodeType = type;
        executeResult = new ExecuteResult(ExecuteResultState.None);
    }

    public virtual NodeState GetNodeState()
    {
        return this.nodeState;
    }

    public virtual void SetNodeState(NodeState state)
    {
        this.nodeState = state;
    }

    public IDisposable Subscribe(IObserver<NodeState> observer)
    {
        nodeObservable = new NodeObservable();
        return nodeObservable.Subscribe(observer);
    }

    public virtual void Init(Int64 id)
    {
        this.id = id;
        SetNodeState(NodeState.Init);
    }

    public virtual void StartProcess()
    {
        SetNodeState(NodeState.Start);
    }

    public virtual INode GetChildNode()
    {
        return childNode;
    }

    public virtual void SetOwner(INode owner)
    {
        this.owner = owner;
    }

    public virtual INode GetOwner()
    {
        return owner;
    }

    public virtual bool HasChild()
    {
        return childNode != null;
    }

    public virtual void OnCompleted()
    {
        SetNodeState(NodeState.Complete);
        executeResult = new ExecuteResult(ExecuteResultState.Success);
        Debug.Assert(nodeObservable != null, "nodeObservableがnullです");
        nodeObservable?.SendComplete();
    }

    public virtual void OnError(Exception error)
    {
        SetNodeState(NodeState.Complete);
        executeResult = new ExecuteResult(ExecuteResultState.Failure);
        Debug.Assert(nodeObservable != null, "ID: "+ GetId() +" type: "+ nodeType +" のnodeObservableがnullです");
        nodeObservable?.SendError(error);
    }

    public virtual void OnNext(NodeState value)
    {
        //switch (value)
        //{
        //    case ActionState.None:
        //        break;
        //    case ActionState.Start:
        //        SetNodeState(NodeState.Start);
        //        nodeObservable.SendState(GetNodeState());
        //        break;
        //    case ActionState.Running:
        //        SetNodeState(NodeState.Running);
        //        nodeObservable.SendState(GetNodeState());
        //        break;
        //    case ActionState.Finished:
        //        SetNodeState(NodeState.Complete);
        //        nodeObservable.SendState(GetNodeState());
        //        break;
        //    default:
        //        Debug.Assert(false, "undefined NodeState in Node.OnNext");
        //        break;
        //}

        //Debug.Log("NodeState OnNext: " + value);
    }

    public virtual ExecuteResult GetExecuteResultState()
    {
        return executeResult;
    }

    public virtual void DoNext()
    {
    }

    public virtual void Run()
    {
        SetNodeState(NodeState.Running);
    }

    public virtual void Activate()
    {
        SetNodeState(NodeState.Init);
        if (childNode != null) {
            childNode.Activate();
        }
    }

    public virtual void Deactivate()
    {
        SetNodeState(NodeState.Disable);
        if (nodeDisposable != null) {
            nodeDisposable.Dispose();
        }
    }

    public abstract void AddNode(INode node);
    public abstract bool IsRoot();
}
