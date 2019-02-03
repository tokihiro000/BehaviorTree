using System;
public interface IFactory<T, U>
    where T : class
    where U : struct
{
    T Create(U type);
    bool Validate(Int64 id, T target);
    void Register(Int64 id, T target);
}