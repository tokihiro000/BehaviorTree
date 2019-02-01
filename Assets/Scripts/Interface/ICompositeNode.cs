using System.Collections.Generic;

public interface ICompositeNode
{
    List<INode> GetChildNodeList();
    bool IsChildComplete();
    void AddNode(INode node, int priority);
}
