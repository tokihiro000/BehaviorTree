using System;

public class ActionObservable : IObservable<ActionState>
{
    public IObserver<ActionState> observer;
    public ActionObserverDisposer disposer;
    
    public void SendState(ActionState state)
    {
        observer.OnNext(state);
    }

    public void SendError(Exception error)
    {
        observer.OnError(error);
    }

    public void SendComplete()
    {
        observer.OnCompleted();
    }

    //監視人を割り当てて監視開始
    public IDisposable Subscribe(IObserver<ActionState> observer)
    {
        observer = (ActionObserver)observer;
        disposer = new ActionObserverDisposer(this);
        return disposer;
    }
}