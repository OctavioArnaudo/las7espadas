using System;
using UnityEngine;

/// <summary>
/// Marks a trigger as a VictoryZone, usually used to end the current game level.
/// </summary>
public class VictoryZone : GroundZone
{
    protected override Action<GameObject> OnProcessed => DetectionHandler;
    protected override void DetectionHandler(GameObject gameObject)
    {
        base.DetectionHandler(gameObject);
        var p = gameObject.GetComponent<MonoController>();
        if (p != null)
        {
            var ev = Simulation.Schedule<PlayerEnteredVictoryZone>();
            ev.controller = this;
            if (p.CompareTag("Goal"))
            {
                QuitGame();
            }
        }
    }
}