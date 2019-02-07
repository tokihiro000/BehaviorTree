using System;
public interface IActionable {
    ActionType ActionType
    {
        get;
    }

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
