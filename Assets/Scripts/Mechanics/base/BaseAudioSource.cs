using UnityEngine;

[
    RequireComponent(
        typeof(AudioSource)
    )
]
public class BaseAudioSource : BaseAnimator
{
    [SerializeField] protected AudioSource Source;
    [SerializeField] protected AudioClip Clip;
}
