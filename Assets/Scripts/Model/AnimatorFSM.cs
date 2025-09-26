public class AnimatorFSM
{
    private AnimatorState currentState;

    public void ChangeState(AnimatorState newState)
    {
        if (newState == null || newState == currentState) return;

        currentState?.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }

    public void Update() => currentState?.OnUpdate();
    public AnimatorState GetCurrentState() => currentState;
}
