public class ActionDisable : ActionEnable
{
    protected override void OnDisable()
    {
        base.OnDisable();
        displacementAction.Disable();
        jumpAction.Disable();
    }
}