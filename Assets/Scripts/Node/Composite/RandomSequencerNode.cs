using System;
using System.Collections.Generic;
using UnityEngine;

public class RandomSequencerNode : Node, IObserver<NodeState>, ICompositeNode
{
    public override NodeType NodeType => this.nodeType = NodeType.RandomSequencer;

    /// <summary>
    /// 子ノードのリスト
    /// </summary>
    private List<INode> childNodeList;

    /// <summary>
    /// 未実行の子ノードが入っているキュー
    /// Activate時にリストにあるノードが全部ぶっこまれる
    /// </summary>
    private List<INode> executeNodeList;

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
    protected internal RandomSequencerNode() : base()
    {
        childNodeList = new List<INode>();
        childNodeDisposerDict = new Dictionary<Int64, IDisposable>();
        childNodePriorityDict = new Dictionary<Int64, int>();
        executeNodeList = new List<INode>();
    }

    public override void Activate()
    {
        executeNodeList.Clear();
        foreach (INode node in childNodeList)
        {
            node.Activate();
            executeNodeList.Add(node);
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
        var childCount = executeNodeList.Count;
        Debug.Assert(childCount != 0, "ID: " + GetId() + " の実行可能子ノードリストが空です");
        var selectedIndex = UnityEngine.Random.Range(0, childCount);
        childNode = executeNodeList[selectedIndex];
        executeNodeList.RemoveAt(selectedIndex);

        Debug.Assert(childNode != null, "Random Sequencer Node ID: " + GetId() + " が選択した子ノードがnullです");
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
        // 子ノードのどれかが失敗したら失敗
        var childeResult = childNode.GetExecuteResultState();
        if (childeResult.GetExecuteResult() != ExecuteResultState.Success)
        {
            SetNodeState(NodeState.Complete);
            executeResult = childeResult;
            nodeObservable?.SendComplete();
            return;
        }

        // 子ノード成功かつまだ実行可能な子ノードがあるならつづける
        if (!IsChildComplete())
        {
            return;
        }

        Debug.Assert(childNode != null, "childNodeがnullです");
        Debug.Assert(childNode.GetNodeState() == NodeState.Complete, "なんか完了してない子供がアサインされている");
        Debug.Assert(nodeObservable != null, "nodeObservableがnullです");

        Debug.Log("Random Sequencer Node OnCompleted");
        SetNodeState(NodeState.Complete);
        executeResult = childeResult;
        nodeObservable?.SendComplete();
    }

    public virtual List<INode> GetChildNodeList()
    {
        return childNodeList;
    }

    public virtual bool IsChildComplete()
    {
        return executeNodeList.Count == 0;
    }
}
