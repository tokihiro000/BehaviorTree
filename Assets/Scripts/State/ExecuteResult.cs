using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteResult {
    private ExecuteResultState state;
    public ExecuteResult(ExecuteResultState state) {
        this.state = state;
    }

    public ExecuteResultState GetExecuteResult() 
    {
        return state;
    }
}
