using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class BasePlayerInput : BaseParticle
{
    [SerializeField] protected PlayerInput playerInput;
    [SerializeField] protected InputActionAsset playerInputActions;
    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
        playerInputActions = playerInput.actions;
    }
}
