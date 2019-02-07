using System;
using System.Collections.Generic;
using UnityEngine;

public class RandomNode : Node, IObserver<NodeState>, ICompositeNode
{
    public override NodeType NodeType => this.nodeType = NodeType.Random;

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
    internal RandomNode() : base()
    {
        childNodeList = new List<INode>();
        childNodeDisposerDict = new Dictionary<Int64, IDisposable>();
        childNodePriorityDict = new Dictionary<Int64, int>();
    }

    public override void Activate()
    {
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
        SetNodeState(NodeState.WaitForChild);
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
        var childCount = childNodeList.Count;
        Debug.Assert(childCount != 0, "ID: " + GetId() + " は子ノードを持っていません");

        var selectedIndex = UnityEngine.Random.Range(0, childCount);
        childNode = childNodeList[selectedIndex];
        Debug.Assert(childNode.GetNodeState() == NodeState.Init, "Node ID: " + GetId() + " が返そうとした子ノードが初期状態ではありません");

        Debug.Log("RandomNodeが選択したインデックス: " + selectedIndex);
        return childNode;
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
