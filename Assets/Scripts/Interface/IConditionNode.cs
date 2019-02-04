using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConditionNode {
    IConditionable Condition
    {
        get;
        set;
    }
}
