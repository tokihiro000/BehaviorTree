using System;

public class NodeObservable : IObservable<NodeState>
{
    public NodeObserverDisposer disposer;
    public NodeObserver observer;

    //監視される仕事を実行
    public void Execute()
    {

        //Console.WriteLine("労働者「作業1をしました」");
        ////監視人に通知
        //if (myObserver != null)
        //    myObserver.OnNext(1);

        //Console.WriteLine("労働者「作業2をしました」");
        ////監視人に通知
        //if (myObserver != null)
        //    myObserver.OnNext(2);

        //Console.WriteLine("労働者「作業3をしました」");
        ////監視人に通知
        //if (myObserver != null)
        //    myObserver.OnNext(3);

        //Console.WriteLine("労働者「すべての作業おわりました」");
        ////監視人に完了通知
        //if (myObserver != null)
            //myObserver.OnCompleted();

    }

    //監視人を割り当てて監視開始
    public IDisposable Subscribe(IObserver<NodeState> observer)
    {
        observer = (NodeObserver)observer;
        disposer = new NodeObserverDisposer(this);
        return disposer;
    }
}