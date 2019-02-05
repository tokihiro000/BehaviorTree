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

    public T Create<T>(NodeType type)
        where T : Node
    {
        var c = (T)Create(type);
        return c;
    }

    public Node Create(NodeType type) {
        Node node = null;
        switch (type)
        {
            case NodeType.Root:
                node = new RootNode();
                break;
            case NodeType.Action:
                node = new ActionNode();
                break;
            case NodeType.Sequencer:
                node = new SequencerNode();
                break;
            case NodeType.Selector:
                node = new SelectorNode();
                break;
            case NodeType.Decorator:
                node = new DecoratorNode();
                break;
            case NodeType.Condition:
                node = new ConditionNode();
                break;
            case NodeType.ConditionWhile:
                node = new ConditionWhileNode();
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