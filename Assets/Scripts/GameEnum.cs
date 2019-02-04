public enum NodeState
{
    None,
    Init,
    Start,
    Running,
    WaitForChild,
    Complete,
    Disable
}

public enum NodeType
{
    Root,
    Selector,
    Sequencer,
    Action,
    Decorator,
    Condition
}

public enum ActionState
{
    None,
    Start,
    Running,
    Finished
}

public enum ExecuteResultState
{
    None,
    Success,
    Failure
}

public enum DecoratorType
{
    Not,
    True,
    False
}

public enum ActionType
{
    Sample1,
    Sample2
}

public enum ConditionType
{
    Sample1
}

public enum FactoryType
{
    Node,
    Action,
    Decorator,
    Condition
}