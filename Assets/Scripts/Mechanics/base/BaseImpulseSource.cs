using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class BaseImpulseSource : BaseContactFilter2D
{
    [SerializeField] protected CinemachineImpulseSource impulseSource;

    public float duration = 0.5f;
    public float magnitude = 0.3f;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    IEnumerator Shake()
    {
        impulseSource.GenerateImpulse();

        float elapsed = 0f;
        Vector3 originalPos = transform.localPosition;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}