using UnityEngine.InputSystem;

public class ActionMapping : InitTarget
{
    protected InputAction displacementAction;
    protected InputAction jumpAction;
    protected InputAction spawnAction;
    protected InputAction defeatAction;
    protected InputAction hurtAction;
    protected InputAction victoryAction;

    protected override void Awake()
    {
        base.Awake();
        displacementAction = InputSystem.actions.FindAction("Player/Move");
        jumpAction = InputSystem.actions.FindAction("Player/Jump");
        spawnAction = InputSystem.actions.FindAction("Player/Spawn");
        defeatAction = InputSystem.actions.FindAction("Player/Die");
        hurtAction = InputSystem.actions.FindAction("Player/Hurt");
        victoryAction = InputSystem.actions.FindAction("Player/Victorious");
    }
}