using System;

public class ActionObserver : IObserver<ActionState>
{
    //監視対象が処理を完了したとき
    public void OnCompleted()
    {
        Console.WriteLine(" 監視人「すべての作業終了を確認した」");
    }

    //監視対象がエラーを出した時
    public void OnError(Exception error)
    {
        Console.WriteLine(" 監視人「エラー発生を確認した」 {0}", error);
    }

    //監視対象から通知が来た時
    public void OnNext(ActionState value)
    {
        Console.WriteLine(" 監視人「作業{0}を確認した」", value);
    }
}
