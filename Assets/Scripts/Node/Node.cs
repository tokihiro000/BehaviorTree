using NodeId = System.Int64;

public abstract class Node
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
    ///  ノードID
    /// </summary>
    private NodeId id;
    public NodeId Id {
        get {
            return this.id;
        }
    }

    protected Node(NodeType type, NodeState state) {
        this.nodeType = type;
        this.nodeState = state;
    }

    public virtual NodeState GetNodeState() {
        return this.nodeState;
    }

    public virtual void SetNodeState(NodeState state) {
        this.nodeState = state;
    }

    public abstract void Activate();
    public abstract void Deactivate();
    public abstract NodeState OnUpdate();
    //public Node Create(NodeType) {

    //}
}
