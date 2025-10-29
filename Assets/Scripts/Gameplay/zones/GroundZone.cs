using UnityEngine;

public class GroundZone : MonoController
{
    protected virtual void DetectionHandler(GameObject gameObject)
    {
        if (gameObject.CompareTag("Ground"))
        {
            ReloadGame();
        }
    }
}
