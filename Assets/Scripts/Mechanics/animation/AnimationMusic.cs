public class AnimationMusic : AnimationMapping
{
    public override void TriggerAudioState(string stateName)
    {
        base.TriggerAudioState(stateName);
        if (stateAudioMap.TryGetValue(stateName, out var audioState))
        {
            audioState.Invoke();
        }
    }
}