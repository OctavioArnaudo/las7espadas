using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class allows an audio clip to be played during an animation state.
/// </summary>
[Serializable]
public class AudioModel : StateMachineModel
{
    /// <summary>
    /// The point in normalized time where the clip should play.
    /// </summary>
    float t = 0.5f;
    /// <summary>
    /// If greater than zero, the normalized time will be (normalizedTime % modulus).
    /// This is used to repeat the audio clip when the animation state loops.
    /// </summary>
    float modulus = 0f;
    /// <summary>
    /// The last normalized time to check if the clip should be played.
    /// </summary>
    float last_t = -1f;

    /// <summary>
    /// The audio clip to be played.
    /// </summary>
    public AudioClip Clip;
    public Func<AudioClip, AudioClip> ClipAction;

    /// <summary>
    /// 
    /// Called when the state machine enters the state.
    /// 
    /// </summary>
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

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

        AnimV1 anim = new AnimV1(animComponent);
        foreach (var kvp in stateAudioMap)
        {
            if (anim.WasInState(kvp.Key))
            {
                if (stateId != kvp.Key)
                {
                    stateId = kvp.Key;
                    kvp.Value.Invoke();
                }
                return;
            }
        }

        if (stateId != "")
        {
            stateId = "";
            Source.Stop();
        }

        foreach (var item in stateAudioList)
        {
            stateAudioMap[item.key] = item.value;
        }

    }

    public AudioModel(AudioClip clip, Func<AudioClip, AudioClip> actionClip)
    {
        Clip = clip;
        ClipAction = actionClip;
    }

    public virtual void Invoke()
    {
        ClipAction?.Invoke(Clip);
    }

    public string stateId;

    protected Dictionary<string, (AudioClip Clip, Func<AudioClip, AudioClip> Action)> stateAudioMap0;
    protected Dictionary<string, AudioModel> stateAudioMap;
    protected List<(string key, AudioModel value)> stateAudioList;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.SendMessage("OnAnimatorStateEnter", stateId, SendMessageOptions.DontRequireReceiver);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.SendMessage("OnAnimatorStateExit", stateId, SendMessageOptions.DontRequireReceiver);
    }

    public virtual IEnumerator TriggerAudioState(string stateName)
    {
        if (stateAudioMap.TryGetValue(stateName, out var audioState))
        {
            audioState.Invoke();
        }
        yield return null;
    }

    public AudioClip PlayLoop(AudioClip clip)
    {
        return PlayLoop(clip, Source, controller.gameObject);
    }
    public AudioClip PlayLoop(AudioClip clip, AudioSource source, GameObject obj)
    {
        AudioSource finalSource = source ?? Source;
        GameObject finalObj = obj ?? controller.gameObject;

        if (finalSource == null && obj != null)
        {
            finalSource = finalObj.GetComponent<AudioSource>();
        }

        if (clip == null && finalSource != null)
        {
            clip = finalSource.clip;
        }

        if (clip == null)
        {
            Debug.LogWarning("AudioClip is null. Please provide a valid AudioClip.");
            return null;
        }

        if (finalSource != null)
        {
            finalSource.clip = clip;
            finalSource.loop = true;
            finalSource.Play();
        }
        else
        {
            Debug.LogWarning($"AudioSource is not set for clip '{clip.name}'.");
        }

        return clip;
    }

    public AudioClip PlayOnce(AudioClip clip)
    {
        return PlayOnce(clip, Source, controller.gameObject);
    }
    public AudioClip PlayOnce(AudioClip clip, AudioSource source, GameObject obj)
    {
        AudioSource finalSource = source ?? Source;
        GameObject finalObj = obj ?? controller.gameObject;

        if (finalSource == null && obj != null)
        {
            finalSource = finalObj.GetComponent<AudioSource>();
        }

        if (clip == null && finalSource != null)
        {
            clip = finalSource.clip;
        }

        if (clip == null)
        {
            Debug.LogWarning("AudioClip is null. Please provide a valid AudioClip.");
            return null;
        }

        if (finalSource != null)
        {
            finalSource.loop = false;
            finalSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"AudioSource or AudioClip '{clip?.name}' is not set.");
        }

        return clip;
    }

}