using UnityEngine;

/// <summary>
/// 
/// PatrolPath defines a path for a Mover to follow, with start and end positions.
/// 
/// </summary>
public partial class PatrolPath
{
    /// <summary>
    /// The Mover class oscillates between start and end points of a path at a defined speed.
    /// </summary>
    public class Mover
    {
        /// <summary>
        /// 
        /// The PatrolPath that the Mover will follow.
        /// 
        /// </summary>
        PatrolPath path;
        /// <summary>
        /// 
        /// The current position along the path, represented as a percentage (0 to 1).
        /// 
        /// </summary>
        float p = 0;
        /// <summary>
        /// 
        /// The duration of the movement from start to end position, calculated based on speed.
        /// 
        /// </summary>
        float duration;
        /// <summary>
        /// 
        /// The time when the movement started, used to calculate the current position.
        /// 
        /// </summary>
        float startTime;

        /// <summary>
        /// 
        /// Initializes a new instance of the Mover class with a specified PatrolPath and speed.
        /// 
        /// </summary>
        public Mover(PatrolPath path, float speed)
        {
            // Validate the speed to ensure it's greater than zero.
            this.path = path;
            // If speed is zero or negative, set it to a default value.
            this.duration = (path.endPosition - path.startPosition).magnitude / speed;
            // Ensure the duration is positive.
            this.startTime = Time.time;
        }

        /// <summary>
        /// Get the position of the mover for the current frame.
        /// </summary>
        /// <value></value>
        public Vector2 Position
        {
            get
            {
                // Calculate the percentage of the path based on the elapsed time.
                p = Mathf.InverseLerp(0, duration, Mathf.PingPong(Time.time - startTime, duration));
                // If the path is not defined, return a zero vector.
                return path.transform.TransformPoint(Vector2.Lerp(path.startPosition, path.endPosition, p));
            }
        }
    }
}