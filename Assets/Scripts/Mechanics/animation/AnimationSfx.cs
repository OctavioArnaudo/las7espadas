public class AnimationSfx : AnimationMusic
{
    public override void TriggerAttackEffect()
    {
        base.TriggerAttackEffect();
        TriggerSfxParticle("Attack");
        TriggerAudioState("Attack");
    }
    public override void TriggerDeathEffect()
    {
        base.TriggerDeathEffect();
        TriggerSfxParticle("Death");
        TriggerAudioState("Death");
    }
    public override void TriggerIdleEffect()
    {
        base.TriggerIdleEffect();
        TriggerSfxParticle("Idle");
        TriggerAudioState("Idle");
    }
    public override void TriggerWalkEffect()
    {
        base.TriggerWalkEffect();
        TriggerSfxParticle("Walk");
        TriggerAudioState("Walk");
    }
    public void TriggerSfxParticle(string stateName)
    {
        if (particleSfxMap.TryGetValue(stateName, out var audioState))
        {
            audioState.Invoke();
        }
    }
}