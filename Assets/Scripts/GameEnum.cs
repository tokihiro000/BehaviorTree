public enum NodeState
{
    None,
    Init,
    Start,
    Running,
    Complete,
    Disable
}

public enum NodeType
{
    Root,
    Selector,
    Sequencer,
    Action
}

public enum ActionState
{
    None,
    Start,
    Running,
    Finished
}
