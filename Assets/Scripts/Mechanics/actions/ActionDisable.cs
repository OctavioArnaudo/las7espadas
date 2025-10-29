public class ActionDisable : ActionEnable
{
    protected override void OnDisable()
    {
        base.OnDisable();
        if (displacementAction != null) displacementAction.Disable();
        if (jumpAction != null) jumpAction.Disable();
        if (menuAction != null) menuAction.Disable();
    }
}