using UnityEngine;
using System;
using System.Collections.Generic;

public class NodeFactory : INodeFactory
{
    private static int idSeed = 1;
    private Dictionary<Int64, Node> nodeDict;

    public NodeFactory() {
        nodeDict = new Dictionary<Int64, Node>();
    }

    public Node CreateNode(NodeType nodeType)
    {

        Node node = null;
        switch (nodeType)
        {
            case NodeType.Root:
                node = new RootNode(nodeType);
                break;
            case NodeType.Action:
                node = new ActionNode(nodeType);
                break;
            case NodeType.Sequencer:
                node = new SequencerNode(nodeType);
                break;
            case NodeType.Selector:
                node = new SelectorNode(nodeType);
                break;
            default:
                Debug.Assert(false, "未定義のノードタイプ");
                break;
        }

        if (node != null)
        {
            node.Init(idSeed);
            RegisterNode(idSeed, node);
            idSeed += 1;
        }

        return node;
    }

    public bool ValidateNode(Int64 id, Node node) {
        Debug.Assert(nodeDict.ContainsKey(id), "指定したノードがNodeFactoryに登録されていません");
        return nodeDict.ContainsKey(id);
    }

    private void RegisterNode(Int64 id, Node node)
    {
        Debug.Assert(!nodeDict.ContainsKey(id), "NodeId: " + id + " のノードはすでに登録されています");
        nodeDict.Add(id, node);
    }
}