public class ActionEnable : ActionMapping
{
    protected override void OnEnable()
    {
        base.OnEnable();
        displacementAction.Enable();
        jumpAction.Enable();
    }
}