using UnityEngine;

[
    RequireComponent(
        typeof(AudioSource)
    )
]
public class BaseAudioSource : BaseAnimator
{
    /// <summary>
    /// 
    /// The audio source component used to play the sound.
    /// 
    /// </summary>
    /*internal new*/
    [SerializeField] protected AudioSource Source;
}
