using System;
using System.Collections;

public abstract class Action : IActionable {
    public IDisposable ActionDisposer => observable.disposer;
    protected ActionObservable observable;
    protected ActionResult actionResult;
    private Int64 actionId;
    private ActionType actionType;

    protected Action(ActionType type)
    {
        this.actionType = type;
        this.actionResult = new ActionResult(ActionResultState.None);
    }

    public void Init(Int64 id)
    {
        this.actionId = id;
    }

    public Int64 GetId()
    {
        return this.actionId;
    }

    public IDisposable Subscribe(IObserver<ActionState> observer) {
        observable = new ActionObservable();
        return observable.Subscribe(observer);
    }

    public abstract void Invoke();
    public ActionResult GetResult()
    {
        return this.actionResult;
    }
}
