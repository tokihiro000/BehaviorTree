using System.Collections;
using UnityEngine;

public class SampleAction : Action
{
    public override void Invoke()
    {
        CoroutineHandler.StartStaticCoroutine(DoSampleAction());
    }

    IEnumerator DoSampleAction() {
        Debug.Log("1");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("2");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("3");
        observable.SendComplete();
    }
}
