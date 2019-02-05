using System;
public interface IActionable {
    IDisposable ActionDisposer
    {
        get;
    }

    void Init(Int64 id);
    void Activate();
    Int64 GetId();
    IDisposable Subscribe(IObserver<ActionState> observer);
    void Invoke();
    ActionResult GetResult();
}
