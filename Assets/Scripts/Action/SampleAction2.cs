using System.Collections;
using UnityEngine;

public class SampleAction2 : Action
{
    public override ActionType ActionType => this.actionType = ActionType.Sample1;

    internal protected SampleAction2() : base()
    {
    }

    public override void Invoke()
    {
        CoroutineHandler.StartStaticCoroutine(DoSampleAction());
    }

    IEnumerator DoSampleAction()
    {
        Debug.Log("4");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("5");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("6");
        actionResult = new ActionResult(ActionResultState.Success);
        observable.SendComplete();
    }
}
