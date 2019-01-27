using System;
public interface IActionable {
    IDisposable ActionDisposer
    {
        get;
    }

    IDisposable Subscribe(IObserver<ActionState> observer);
    void Invoke();
}
