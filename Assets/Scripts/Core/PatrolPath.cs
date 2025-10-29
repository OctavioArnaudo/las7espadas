using UnityEngine;

/// <summary>
/// This component is used to create a patrol path, two points which enemies will move between.
/// </summary>
public partial class PatrolPath : MonoBehaviour
{
    /// <summary>
    /// One end of the patrol path.
    /// </summary>
    public Vector2 startPosition, endPosition;

    /// <summary>
    /// Create a Mover instance which is used to move an entity along the path at a certain speed.
    /// </summary>
    /// <param name="speed"></param>
    /// <returns></returns>
    public Mover CreateMover(float speed = 1) => new Mover(this, speed);

    void Reset()
    {
        // Set the start and end positions to the left and right of the patrol path object.
        startPosition = Vector3.left;
        // Set the end position to the right of the patrol path object.
        endPosition = Vector3.right;
    }
}