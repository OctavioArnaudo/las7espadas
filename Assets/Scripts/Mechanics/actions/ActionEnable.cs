public class ActionEnable : ActionMapping
{
    protected override void OnEnable()
    {
        base.OnEnable();
        if(displacementAction != null) displacementAction.Enable();
        if(jumpAction != null) jumpAction.Enable();
        if(menuAction != null) menuAction.Enable();
    }
}