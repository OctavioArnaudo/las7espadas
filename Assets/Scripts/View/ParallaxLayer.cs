using UnityEngine;

/// <summary>
/// Used to move a transform relative to the main camera position with a scale factor applied.
/// This is used to implement parallax scrolling effects on different branches of gameobjects.
/// </summary>
public class ParallaxLayer : MonoBehaviour
{
    /// <summary>
    /// Movement of the layer is scaled by this value.
    /// </summary>
    public Vector3 movementScale = Vector3.one;

    /// <summary>
    /// 
    /// The camera transform used to calculate the position of this layer.
    /// 
    /// </summary>
    Transform _camera;

    /// <summary>
    /// 
    /// Called when the script instance is being loaded. It initializes the camera transform to the main camera.
    /// 
    /// </summary>
    void Awake()
    {
        // Initialize the camera transform to the main camera
        _camera = Camera.main.transform;
    }

    /// <summary>
    /// 
    /// Called once per frame after all Update methods have been called. It updates the position of the transform
    /// 
    /// based on the camera's position and the movement scale.
    /// 
    /// </summary>
    void LateUpdate()
    {
        // Update the position of the transform based on the camera's position and the movement scale
        transform.position = Vector3.Scale(_camera.position, movementScale);
    }
}
