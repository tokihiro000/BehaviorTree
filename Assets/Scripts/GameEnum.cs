﻿public enum NodeState
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
    None,
    Root,
    Selector,
    Sequencer,
    Random,
    RandomSelector,
    RandomSequencer,
    Action,
    Decorator,
    Condition,
    ConditionWhile
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
    Failure,
    Error
}

public enum ActionResultState
{
    None,
    Success,
    Failure
}

public enum DecoratorType
{
    None,
    Not,
    True,
    False
}

public enum ActionType
{
    None,
    Sample1,
    Sample2
}

public enum ConditionType
{
    None,
    Sample1,
    Repeat
}

public enum FactoryType
{
    Node,
    Action,
    Decorator,
    Condition
}