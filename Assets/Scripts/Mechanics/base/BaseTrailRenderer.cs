using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class BaseTrailRenderer : BaseTilemap
{
    [SerializeField] protected TrailRenderer trail;
    public float trailDuration = 0.5f;
    public float startWidth = 0.3f;
    public float endWidth = 0f;
    [SerializeField] protected Gradient gradient;
    [SerializeField] protected Material material;

    protected override void Awake()
    {
        base.Awake();
        trail = GetComponent<TrailRenderer>();
    }

    void SetupTrail()
    {
        trail.time = trailDuration;
        trail.startWidth = startWidth;
        trail.endWidth = endWidth;

        if (material == null)
        {
            material = new Material(Shader.Find("Particles/Additive"));
            material.color = Color.red;
        }

        trail.material = material;

        if (gradient == null)
        {
            gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(Color.yellow, 0f),
                    new GradientColorKey(Color.red, 0.5f),
                    new GradientColorKey(new Color(0.5f, 0f, 0f), 1f)
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(0.5f, 0.5f),
                    new GradientAlphaKey(0f, 1f)
                }
            );
        }

        trail.colorGradient = gradient;
    }
}
