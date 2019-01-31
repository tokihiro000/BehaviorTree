using System;
using System.Collections;

public abstract class AbstractAction : IActionable {
    protected ActionObservable observable;
    public IDisposable ActionDisposer
    {
        get {
            return observable.disposer;
        }
    }

    public IDisposable Subscribe(IObserver<ActionState> observer) {
        observable = new ActionObservable();
        return observable.Subscribe(observer);
    }

    public abstract void Invoke();
}
