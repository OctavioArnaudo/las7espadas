using UnityEngine.InputSystem;

public class ActionMapping : InitTrail
{
    protected InputAction defeatAction;
    protected InputAction displacementAction;
    protected InputAction hurtAction;
    protected InputAction jumpAction;
    protected InputAction menuAction;
    protected InputAction spawnAction;
    protected InputAction victoryAction;

    protected override void Awake()
    {
        base.Awake();
        displacementAction = InputSystem.actions.FindAction("Player/Move");
        jumpAction = InputSystem.actions.FindAction("Player/Jump");
        menuAction = InputSystem.actions.FindAction("UI/Menu");
        spawnAction = InputSystem.actions.FindAction("Player/Spawn");
        defeatAction = InputSystem.actions.FindAction("Player/Die");
        hurtAction = InputSystem.actions.FindAction("Player/Hurt");
        victoryAction = InputSystem.actions.FindAction("Player/Victorious");
    }
}