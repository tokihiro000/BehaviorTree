using UnityEngine;
using System;
using System.Collections.Generic;

public class NodeFactory : IFactory<Node, NodeType>    
{
    private static int idSeed = 1;
    private Dictionary<Int64, Node> nodeDict;

    public NodeFactory() {
        nodeDict = new Dictionary<Int64, Node>();
    }

    public Node Create(NodeType type) {
        Node node = null;
        switch (type)
        {
            case NodeType.Root:
                node = new RootNode(type);
                break;
            case NodeType.Action:
                node = new ActionNode(type);
                break;
            case NodeType.Sequencer:
                node = new SequencerNode(type);
                break;
            case NodeType.Selector:
                node = new SelectorNode(type);
                break;
            case NodeType.Decorator:
                node = new DecoratorNode(type);
                break;
            case NodeType.Condition:
                node = new ConditionNode(type);
                break;
            default:
                Debug.Assert(false, "未定義のノードタイプ");
                break;
        }

        if (node != null)
        {
            node.Init(idSeed);
            Register(idSeed, node);
            idSeed += 1;
        }

        return node;
    }

    public bool Validate(Int64 id, Node node) {
        Debug.Assert(nodeDict.ContainsKey(id), "指定したノードがNodeFactoryに登録されていません");
        return nodeDict.ContainsKey(id);
    }

    public void Register(Int64 id, Node node)
    {
        Debug.Assert(!nodeDict.ContainsKey(id), "NodeId: " + id + " のノードはすでに登録されています");
        nodeDict.Add(id, node);
    }
}