using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreakable : TransformPointings
{
    [SerializeField] protected float breakablePlatformTime = 1.5f;
    protected Dictionary<GameObject, float> breakablePlatformTimers = new Dictionary<GameObject, float>();
    protected HashSet<GameObject> platformBreaking = new HashSet<GameObject>();

    protected override void Update()
    {
        base.Update();
        PlatformTiming();
    }

    protected virtual void PlatformTiming()
    {
        List<GameObject> toBreak = new List<GameObject>();

        foreach (var platform in new List<GameObject>(breakablePlatformTimers.Keys))
        {
            if (platform == null) continue;
            breakablePlatformTimers[platform] -= Time.deltaTime;

            if (breakablePlatformTimers[platform] <= 0 && !platformBreaking.Contains(platform))
            {
                toBreak.Add(platform);
            }
        }

        foreach (var platform in toBreak)
        {
            PlatformBreak(platform);
            platformBreaking.Add(platform);
            if (breakablePlatformTimers.ContainsKey(platform))
                breakablePlatformTimers.Remove(platform);
        }
    }

    protected virtual void PlatformBreak(GameObject platform)
    {
        Animator platAnim = platform.GetComponent<Animator>();

        if (breakEffectPGO != null)
        {
            Instantiate(breakEffectPGO, platform.transform.position, Quaternion.identity);
        }

        Destroy(platform, 0.5f);
    }

    protected virtual void HandleCollisionOrTrigger(GameObject obj)
    {
        if (obj.CompareTag(tempPlatformPGO.tag) || obj.CompareTag(movingTempPlatformPGO.tag))
        {
            isGrounded = true;
            if (!platformBreaking.Contains(obj))
            {
                breakablePlatformTimers[obj] = breakablePlatformTime;
            }
        }
    }

    protected override Action<GameObject> OnProcessed => DetectionHandler;
    protected override void DetectionHandler(GameObject gameObject)
    {
        base.DetectionHandler(gameObject);
        if (gameObject.CompareTag("TempPlatform") || gameObject.CompareTag("MovingTempPlatform"))
        {
            StartPlatformTimer(gameObject);
        }
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
        if (collision.gameObject.CompareTag("TempPlatform") || collision.gameObject.CompareTag("MovingTempPlatform"))
        {
            if (breakablePlatformTimers.ContainsKey(collision.gameObject))
                breakablePlatformTimers.Remove(collision.gameObject);
        }
    }

    public void StartPlatformTimer(GameObject platform)
    {
        if (platform == null) return;
        if (!platformBreaking.Contains(platform))
        {
            breakablePlatformTimers[platform] = breakablePlatformTime;
        }
    }

}