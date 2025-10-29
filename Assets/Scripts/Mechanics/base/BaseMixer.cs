using UnityEngine;
using UnityEngine.Audio;

[
    RequireComponent(
        typeof(
            AudioMixer
        )
    )
]
public class BaseMixer : BaseLayerMask
{
    [SerializeField] public AudioMixer mixer;
}