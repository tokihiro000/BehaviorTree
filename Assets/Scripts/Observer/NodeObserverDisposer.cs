using System;

public class NodeObserverDisposer : IDisposable
{
    public NodeObservable observable;
    public NodeObserverDisposer(NodeObservable observable)
    {
        this.observable = observable;
    }

    public void Dispose()
    {
        //監視人をはずす
        observable.observer = null;
        Console.WriteLine("  削除人「監視人を削除したぞ」");
    }
}