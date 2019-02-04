using System;
using System.Collections;

public abstract class Action : IActionable {
    protected ActionObservable observable;
    public IDisposable ActionDisposer => observable.disposer;
    private Int64 actionId;
    private ActionType actionType;
    private Int64 id;

    protected Action(ActionType type)
    {
        this.actionType = type;
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
}
