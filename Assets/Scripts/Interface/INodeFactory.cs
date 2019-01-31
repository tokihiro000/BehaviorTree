using System;
interface INodeFactory {
    Node CreateNode(NodeType nodeType);
    bool ValidateNode(Int64 id, Node node);
}
