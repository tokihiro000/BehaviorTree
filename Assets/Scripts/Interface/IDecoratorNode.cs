using System;

public interface IDecoratorNode
{
    ExecuteResult Decorate(ExecuteResult result);
}
