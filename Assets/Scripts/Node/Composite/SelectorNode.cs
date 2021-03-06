﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : Node, IObserver<NodeState>, ICompositeNode
{
    public override NodeType NodeType => this.nodeType = NodeType.Selector;

    /// <summary>
    /// 子ノードのリスト
    /// </summary>
    private List<INode> childNodeList;

    /// <summary>
    /// 各子ノードのIObserberのdisposerを保持する
    /// </summary>
    private Dictionary<Int64, IDisposable> childNodeDisposerDict;

    /// <summary>
    /// 子ノードの優先度
    /// </summary>
    private Dictionary<Int64, int> childNodePriorityDict;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="type">Type.</param>
    internal SelectorNode() : base()
    {
        childNodeList = new List<INode>();
        childNodeDisposerDict = new Dictionary<Int64, IDisposable>();
        childNodePriorityDict = new Dictionary<Int64, int>();
    }

    public override void Activate()
    {
        childNodeList.Sort(
            (a, b) => (childNodePriorityDict[b.GetId()] - childNodePriorityDict[a.GetId()])
        );

        foreach (INode node in childNodeList)
        {
            node.Activate();
        }

        base.Activate();
    }

    public override void StartProcess()
    {
        base.StartProcess();
    }

    public override void Run()
    {
        SetNodeState(IsChildComplete() ? NodeState.Complete : NodeState.WaitForChild);
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    public override void AddNode(INode node)
    {
        node.SetOwner(this);
        childNodeList.Add(node);
        childNodeDisposerDict[GetId()] = node.Subscribe(this);

        var nodeId = node.GetId();
        if (!childNodePriorityDict.ContainsKey(nodeId))
        {
            childNodePriorityDict[nodeId] = 0;
        }
    }

    public virtual void AddNode(INode node, int priority)
    {
        var nodeId = node.GetId();
        childNodePriorityDict[nodeId] = priority;
        AddNode(node);
    }

    public override INode GetChildNode()
    {
        Debug.Assert(childNodeList.Count != 0, "ID: " + GetId() + " は子ノードを持っていません");
        foreach (INode node in childNodeList)
        {
            NodeState nodeState = node.GetNodeState();
            if (nodeState == NodeState.Init)
            {
                childNode = node;
                return childNode;
            }
        }

        Debug.Assert(false, "Selector Node ID: " + GetId() + " は初期状態の子ノードを持っていません");
        return null;
    }

    public override bool IsRoot()
    {
        return false;
    }

    public override bool HasChild()
    {
        return childNodeList.Count != 0;
    }

    public override void OnCompleted()
    {
        // 子ノードのどれかが完了した時のみ完了状態になる
        if (!IsChildComplete())
        {
            return;
        }

        Debug.Assert(childNode != null, "childNodeがnullです");
        Debug.Assert(childNode.GetNodeState() == NodeState.Complete, "なんか完了してない子供がアサインされている");
        Debug.Assert(nodeObservable != null, "nodeObservableがnullです");

        Debug.Log("SelectorNode OnCompleted");
        SetNodeState(NodeState.Complete);
        executeResult = childNode.GetExecuteResultState();
        nodeObservable?.SendComplete();

    }

    public virtual List<INode> GetChildNodeList()
    {
        return childNodeList;
    }

    public virtual bool IsChildComplete()
    {
        foreach (INode node in childNodeList)
        {
            NodeState nodeState = node.GetNodeState();
            if (nodeState == NodeState.Complete)
            {
                return true;
            }
        }

        return false;
    }
}
