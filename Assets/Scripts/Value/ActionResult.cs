public class ActionResult
{
    private ActionResultState state;
    public ActionResult(ActionResultState state)
    {
        this.state = state;
    }

    public ActionResultState GetExecuteResult()
    {
        return state;
    }
}
