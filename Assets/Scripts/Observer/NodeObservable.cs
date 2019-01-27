using System;

public class NodeObservable : IObservable<NodeState>
{
    public NodeObserverDisposer disposer;
    public NodeObserver observer;

    public void SendState(NodeState state)
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
    public IDisposable Subscribe(IObserver<NodeState> observer)
    {
        observer = (NodeObserver)observer;
        disposer = new NodeObserverDisposer(this);
        return disposer;
    }
}