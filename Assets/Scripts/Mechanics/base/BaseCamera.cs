using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class BaseCamera : BaseAudioSource
{
    [SerializeField] protected Camera cam;
    protected override void Awake()
    {
        base.Awake();
        cam ??= GetComponent<Camera>();
    }
}