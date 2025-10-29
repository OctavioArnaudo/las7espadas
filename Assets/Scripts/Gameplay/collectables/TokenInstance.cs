using UnityEngine;

/// <summary>
/// This class contains the data required for implementing token collection mechanics.
/// It does not perform animation of the token, this is handled in a batch by the 
/// TokenController in the scene.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class TokenInstance : MonoBehaviour
{
    /// <summary>
    /// 
    /// Audio clip to play when the token is collected.
    /// 
    /// </summary>
    public AudioClip tokenCollectAudio;
    /// <summary>
    /// 
    /// If true, the token will be collected when the player collides with it.
    /// 
    /// </summary>
    [Tooltip("If true, animation will start at a random position in the sequence.")]
    public bool randomAnimationStartTime = false;
    /// <summary>
    /// 
    /// List of frames that make up the animation when the token is idle.
    /// 
    // </summary>
    [Tooltip("List of frames that make up the animation.")]
    public Sprite[] idleAnimation, collectedAnimation;

    internal Sprite[] sprites = new Sprite[0];

    internal SpriteRenderer _renderer;

    //unique index which is assigned by the TokenController in a scene.
    internal int tokenIndex = -1;
    /// <summary>
    /// 
    /// The controller that manages the token animations in the scene.
    /// 
    // </summary>
    internal TokenController controller;
    //active frame in animation, updated by the controller.
    internal int frame = 0;
    /// <summary>
    /// 
    /// Flag to indicate if the token has been collected.
    ///
    /// </summary>
    internal bool collected = false;

    /// <summary>
    /// 
    /// Called when the script is first loaded or when the game object is instantiated.
    /// 
    /// </summary>
    void Awake()
    {
        //ensure that the token has a collider, and that it is set to trigger.
        _renderer = GetComponent<SpriteRenderer>();
        // if the sprite renderer is not found, add one.
        if (_renderer == null)
        {
            // if the sprite renderer is not found, add one.
            _renderer = gameObject.AddComponent<SpriteRenderer>();
        }
        // ensure that the token has a collider, and that it is set to trigger.
        var collider = GetComponent<Collider2D>();
        // if the collider is not found, add one.
        if (randomAnimationStartTime)
            // if random animation start time is enabled, set the frame to a random value.
            frame = Random.Range(0, sprites.Length);
        // if the collider is not found, add one.
        sprites = idleAnimation;
    }

    /// <summary>
    /// 
    /// Called when the script is enabled. It sets the sprite renderer's sprite to the first frame of the animation.
    /// 
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        //only exectue OnPlayerEnter if the player collides with this token.
        var player = other.gameObject.GetComponent<MonoController>();
        //if the player is not null, call OnPlayerEnter.
        if (player != null) OnPlayerEnter(player);
    }

    /// <summary>
    /// 
    /// Called when the player collides with the token. It disables the token, plays the collect audio, and sends an event.
    /// 
    /// </summary>
    void OnPlayerEnter(MonoController player)
    {
        //if the token has already been collected, do nothing.
        if (collected) return;
        //disable the gameObject and remove it from the controller update list.
        frame = 0;
        //play the token collect audio.
        sprites = collectedAnimation;
        //ensure that the token is not animated.
        if (controller != null)
            // remove the token from the controller's list of tokens.
            collected = true;
        //send an event into the gameplay system to perform some behaviour.
        var ev = Simulation.Schedule<PlayerTokenCollision>();
        //create a new PlayerTokenCollision event.
        ev.token = this;
        //set the token property of the event to this token instance.
        ev.player = player;
    }
}