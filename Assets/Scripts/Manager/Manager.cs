/*
 * 参考というか完全にこれが欲しかったので下記のものを使わせていただきますm(_ _)m 
 * http://waken.hatenablog.com/entry/2016/03/05/102928
 */
using UnityEngine;

public class Manager<T> where T : class, new()
{
    protected Manager()
    {
        Debug.Assert(null == instance, "Managerクラスは直接newすることができません");
    }

    private static readonly T instance = new T();
    public static T Instance => instance;
}