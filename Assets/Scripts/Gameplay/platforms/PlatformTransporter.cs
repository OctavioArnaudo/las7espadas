using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTransporter : PlatformBreakable
{
    [SerializeField] private float transportVelocity;
    [SerializeField] private string transportTag = "Moving";
    [SerializeField] private List<Transform> transportPoints;
    private int nextPlatform = 1;
    private bool orderPlatforms= true;

    protected override void Awake()
    {
        base.Awake();
        if (transformsByTag.ContainsKey(transportTag))
        {
            transportPoints = transformsByTag[transportTag];
        }
        else
        {
            Debug.LogWarning($"No platforms found with tag '{transportTag}'. Please ensure platforms are tagged correctly.");
        }
    }

    protected override void Update()
    {
        base.Update();
        if(orderPlatforms && nextPlatform >= transportPoints.Count -1)
        {
            orderPlatforms = false;
        }
        else if(!orderPlatforms && nextPlatform <= 0)
        {
            orderPlatforms = true;
        }

        if (Vector2.Distance(transform.position, transportPoints[nextPlatform].position) <0.1f)
        {
            if(orderPlatforms)
            {
                nextPlatform += 1;
            }
            else
            {
                nextPlatform -= 1;
            }
        }
        transform.position= Vector2.MoveTowards(transform.position, transportPoints[nextPlatform].position, transportVelocity * Time.deltaTime);
    }

    protected override Action<GameObject> OnProcessed => DetectionHandler;
    protected override void DetectionHandler(GameObject gameObject)
    {
        if (gameObject.CompareTag("Player"))
        {
            gameObject.transform.SetParent(this.transform);
        }
    }
}
