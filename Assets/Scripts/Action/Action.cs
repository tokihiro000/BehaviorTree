using System;
using System.Collections;

public abstract class Action : IActionable {
    protected ActionType actionType;
    public virtual ActionType ActionType => this.actionType = ActionType.None;

    public IDisposable ActionDisposer => observable.disposer;
    protected ActionObservable observable;
    protected ActionResult actionResult;
    private Int64 actionId;

    protected internal Action()
    {
    }

    public void Init(Int64 id)
    {
        this.actionId = id;
    }

    public virtual void Activate()
    {
        this.actionResult = new ActionResult(ActionResultState.None);
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
