using System;

public interface IDecoratorNode
{
    ExecuteResult Decorate(ExecuteResult result);
    //void SetCallbackFunc(Func<string, string> fucn);
}
