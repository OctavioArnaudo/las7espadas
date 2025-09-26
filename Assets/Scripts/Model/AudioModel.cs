using System;
using UnityEngine;

/// <summary>
/// This class allows an audio clip to be played during an animation state.
/// </summary>
public class AudioModel : StateMachineBehaviour
{
    /// <summary>
    /// 
    /// The audio source component used to play the jump sound.
    /// 
    /// </summary>
    /*internal new*/
    public AudioSource Source;
    /// <summary>
    /// The point in normalized time where the clip should play.
    /// </summary>
    public float t = 0.5f;
    /// <summary>
    /// If greater than zero, the normalized time will be (normalizedTime % modulus).
    /// This is used to repeat the audio clip when the animation state loops.
    /// </summary>
    public float modulus = 0f;

    /// <summary>
    /// The audio clip to be played.
    /// </summary>
    public AudioClip Clip;
    public Func<AudioClip, AudioClip> ClipAction;
    /// <summary>
    /// The last normalized time to check if the clip should be played.
    /// </summary>
    float last_t = -1f;

    public ParticleSystem Particle;
    public Func<ParticleSystem, ParticleSystem> ParticleAction;

    /// <summary>
    /// 
    /// Called when the state machine enters the state.
    /// 
    /// </summary>
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If the clip is null, do nothing.
        var nt = stateInfo.normalizedTime;
        // If the normalized time is less than the target time, do nothing.
        if (modulus > 0f) nt %= modulus;
        // If the normalized time is less than the target time, do nothing.
        if (nt >= t && last_t < t)
            // If the normalized time is greater than or equal to the target time and the last normalized time was less than the target time, play the clip.
            AudioSource.PlayClipAtPoint(Clip, animator.transform.position);
        // Update the last normalized time to the current normalized time.
        last_t = nt;
    }

    public AudioModel(AudioClip clip, Func<AudioClip, AudioClip> action)
    {
        Clip = clip;
        ClipAction = action;
    }
    public AudioModel(ParticleSystem particle, Func<ParticleSystem, ParticleSystem> action)
    {
        Particle = particle;
        ParticleAction = action;
    }

    public void Invoke()
    {
        ClipAction?.Invoke(Clip);
        ParticleAction?.Invoke(Particle);
    }

    public string stateId;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.SendMessage("OnAnimatorStateEnter", stateId, SendMessageOptions.DontRequireReceiver);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.SendMessage("OnAnimatorStateExit", stateId, SendMessageOptions.DontRequireReceiver);
    }

}