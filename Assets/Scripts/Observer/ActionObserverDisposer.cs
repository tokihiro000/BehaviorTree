using System;

public class ActionObserverDisposer : IDisposable
{
    public ActionObservable observable;
    public ActionObserverDisposer(ActionObservable observable)
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