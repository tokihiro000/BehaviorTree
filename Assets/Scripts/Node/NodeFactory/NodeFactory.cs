using UnityEngine;

public class NodeFactory : INodeFactory
{
    private static int idSeed = 1;

    public Node CreateNode(NodeType nodeType)
    {
        Node node = null;
        switch (nodeType)
        {
            case NodeType.Action:
                node = new ActionNode(nodeType);
                break;
            default:
                Debug.Assert(false, "未定義のノードタイプ");
                break;
        }

        if (node != null)
        {
            idSeed += 1;
            node.Init(idSeed);
        }

        return node;
    }
}
