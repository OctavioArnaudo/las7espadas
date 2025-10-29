using UnityEngine;

/// <summary>
/// Implements game physics for some in game entity.
/// </summary>
public class KinematicObject : MonoBehaviour
{
    /// <summary>
    /// The minimum normal (dot product) considered suitable for the entity sit on.
    /// </summary>
    public float minGroundNormalY = .65f;

        /// <summary>
        /// A custom gravity coefficient applied to this entity.
        /// </summary>
        public float gravityModifier = 1f;

        /// <summary>
        /// The current velocity of the entity.
        /// </summary>
        public Vector2 velocity;

    /// <summary>
    /// Is the entity currently sitting on a surface?
    /// </summary>
    /// <value></value>
    public bool isGrounded
    {
        get;
        private set;
    }

    /// <summary>
    /// 
    /// The target velocity for the entity, set by the game logic.
    /// 
    /// </summary>
    protected Vector2 targetVelocity;
    /// <summary>
    /// 
    /// The normal of the ground surface the entity is currently on.
    /// 
    /// </summary>
    protected Vector2 groundNormal;
    /// <summary>
    /// 
    /// The Rigidbody2D component used for physics calculations.
    /// 
    /// </summary>
    protected Rigidbody2D body;
    /// <summary>
    /// 
    /// The contact filter used to determine which colliders the entity interacts with.
    /// 
    /// </summary>
    protected ContactFilter2D contactFilter;
    /// <summary>
    /// 
    /// A buffer for storing RaycastHit2D results during movement calculations.
    /// 
    /// </summary>
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    /// <summary>
    /// 
    /// The minimum distance that the entity can move before a movement is considered valid.
    /// 
    /// </summary>
    protected
    const float minMoveDistance = 0.001f;
        /// <summary>
        /// 
        /// The radius of the shell used to prevent tunneling through colliders.
        /// 
        /// </summary>
        protected
        const float shellRadius = 0.01f;

        /// <summary>
        /// Bounce the object's vertical velocity.
        /// </summary>
        /// <param name="value"></param>
        public void Bounce(float value)
    {
        // Set the vertical component of the velocity to the specified value.
        velocity.y = value;
    }

    /// <summary>
    /// Bounce the objects velocity in a direction.
    /// </summary>
    /// <param name="dir"></param>
    public void Bounce(Vector2 dir)
    {
        // Normalize the direction vector to ensure it has a magnitude of 1.
        velocity.y = dir.y;
        // Set the horizontal component of the velocity to the x component of the direction vector.
        velocity.x = dir.x;
    }

    /// <summary>
    /// Teleport to some position.
    /// </summary>
    /// <param name="position"></param>
    public void Teleport(Vector3 position)
    {
        // Set the position of the Rigidbody2D to the specified position.
        body.position = position;
        // Reset the velocity to zero to stop any movement.
        velocity *= 0;
        // Reset the ground normal to zero, indicating no ground contact.
        body.linearVelocity *= 0;
    }

    /// <summary>
    /// 
    /// Called when the script is enabled. It initializes the Rigidbody2D component and sets it to kinematic mode.
    /// 
    /// </summary>
    protected virtual void OnEnable()
    {
        // Initialize the contact filter for physics interactions.
        body = GetComponent<Rigidbody2D>();
        // Ensure the Rigidbody2D component is not null.
        body.bodyType = RigidbodyType2D.Kinematic;
    }

    /// <summary>
    /// 
    /// Called when the script is disabled. It resets the Rigidbody2D component to dynamic mode.
    /// 
    /// </summary>
    protected virtual void OnDisable()
    {
        // Reset the Rigidbody2D component to dynamic mode when the script is disabled.
        body.bodyType = RigidbodyType2D.Dynamic;
    }

    /// <summary>
    /// 
    /// Called at the start of the game. It sets up the contact filter and initializes the ground normal.
    /// 
    /// </summary>
    protected virtual void Start()
    {
        // Initialize the ground normal to zero.
        contactFilter.useTriggers = false;
        // Set the contact filter to ignore triggers.
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        // Set the contact filter to use the layer mask of the game object.
        contactFilter.useLayerMask = true;
    }

    /// <summary>
    /// 
    /// Called every frame. It computes the target velocity based on game logic.
    /// 
    /// </summary>
    protected virtual void Update()
    {
        // Reset the target velocity to zero at the start of each frame.
        targetVelocity = Vector2.zero;
        // Call the method to compute the velocity based on game logic.
        ComputeVelocity();
    }

    /// <summary>
    /// 
    /// Computes the velocity of the entity based on game logic.
    /// 
    /// </summary>
    protected virtual void ComputeVelocity()
    {

    }

    /// <summary>
    /// 
    /// Called at a fixed interval to apply physics calculations.
    /// 
    /// </summary>
    protected virtual void FixedUpdate()
    {
        //if already falling, fall faster than the jump speed, otherwise use normal gravity.
        if (velocity.y < 0)
            // Apply the gravity modifier to the velocity if it is falling.
            velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        else
            // Apply the normal gravity to the velocity if it is not falling.
            velocity += Physics2D.gravity * Time.deltaTime;

        //if we are not grounded, apply the gravity modifier to the velocity.
        velocity.x = targetVelocity.x;

        //if we are not grounded, apply the gravity modifier to the velocity.
        isGrounded = false;

        //if we are not grounded, apply the gravity modifier to the velocity.
        var deltaPosition = velocity * Time.deltaTime;

        //if we are not grounded, apply the gravity modifier to the velocity.
        var moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        // Calculate the movement along the ground based on the ground normal.
        var move = moveAlongGround * deltaPosition.x;

        // Perform the movement along the ground.
        PerformMovement(move, false);

        // Calculate the vertical movement based on the delta position.
        move = Vector2.up * deltaPosition.y;

        // Perform the vertical movement.
        PerformMovement(move, true);

    }

    /// <summary>
    /// 
    /// Performs the movement of the entity based on the specified move vector and whether it is a vertical movement.
    /// 
    /// </summary>
    void PerformMovement(Vector2 move, bool yMovement)
    {
        // If the move vector is zero, return early.
        var distance = move.magnitude;

        // If the distance is less than the minimum move distance, return early.
        if (distance > minMoveDistance)
        {
            //check if we hit anything in current direction of travel
            var count = body.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            // If we hit something, process the hits.
            for (var i = 0; i < count; i++)
            {
                // If the hit distance is less than the shell radius, continue to the next hit.
                var currentNormal = hitBuffer[i].normal;

                //is this surface flat enough to land on?
                if (currentNormal.y > minGroundNormalY)
                {
                    //if so, we are grounded.
                    isGrounded = true;
                    // if moving up, change the groundNormal to new surface normal.
                    if (yMovement)
                    {
                        //if we are moving up, we are grounded.
                        groundNormal = currentNormal;
                        // if we are moving up, we are grounded.
                        currentNormal.x = 0;
                    }
                }
                if (isGrounded)
                {
                    //how much of our velocity aligns with surface normal?
                    var projection = Vector2.Dot(velocity, currentNormal);
                    //if we are moving up, we are grounded.
                    if (projection < 0)
                    {
                        //slower velocity if moving against the normal (up a hill).
                        velocity = velocity - projection * currentNormal;
                    }
                }
                else
                {
                    //We are airborne, but hit something, so cancel vertical up and horizontal velocity.
                    velocity.x *= 0;
                    //if we are not grounded, we are not moving up.
                    velocity.y = Mathf.Min(velocity.y, 0);
                }
                //remove shellDistance from actual move distance.
                var modifiedDistance = hitBuffer[i].distance - shellRadius;
                //if we are moving up, we are grounded.
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        //if we are not grounded, we are not moving up.
        body.position = body.position + move.normalized * distance;
    }

}