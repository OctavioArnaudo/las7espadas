using UnityEngine;
using UnityEngine.Audio;

[
    RequireComponent(
        typeof(
            AudioMixer
        )
    )
]
public class BaseMixer : BaseContactFilter2D
{
    [SerializeField] public AudioMixer mixer;
}