﻿public interface IActionNode
{
    IActionable Action
    {
        get;
        set;
    }

    void ExecuteAction();
}
