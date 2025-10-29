using UnityEngine;

/// <summary>
/// This class animates all token instances in a scene.
/// This allows a single update call to animate hundreds of sprite 
/// animations.
/// If the tokens property is empty, it will automatically find and load 
/// all token instances in the scene at runtime.
/// </summary>
public class TokenController : MonoBehaviour
{
    /// <summary>
    /// 
    /// The frame rate at which the tokens are animated.
    /// 
    // </summary>
    [Tooltip("Frames per second at which tokens are animated.")]
    public float frameRate = 12;
    /// <summary>
    /// 
    /// Instances of tokens which are animated. If empty, token instances are found and loaded at runtime.
    /// 
    // </summary>
    [Tooltip("Instances of tokens which are animated. If empty, token instances are found and loaded at runtime.")]
    public TokenInstance[] tokens;

    /// <summary>
    /// The next frame time is used to determine when to update the token animations.
    /// </summary>
    float nextFrameTime = 0;

    /// <summary>
    /// 
    /// Finds all token instances in the scene and assigns them to the tokens property.
    /// 
    /// </summary>
    [ContextMenu("Find All Tokens")]
    void FindAllTokensInScene()
    {
        //Find all token instances in the scene and assign them to the tokens property.
        tokens = FindObjectsByType<TokenInstance>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    }

    /// <summary>
    /// 
    /// Called when the script instance is being loaded. It initializes the tokens array
    /// 
    /// and registers all token instances with the controller.
    /// 
    /// </summary>
    void Awake()
    {
        //if tokens are empty, find all instances.
        //if tokens are not empty, they've been added at editor time.
        if (tokens.Length == 0)
            //find all token instances in the scene.
            FindAllTokensInScene();
        //Register all tokens so they can work with this controller.
        for (var i = 0; i < tokens.Length; i++)
        {
            //ensure that the token is not null.
            tokens[i].tokenIndex = i;
            //set the token index so it can be used to identify the token.
            tokens[i].controller = this;
        }
    }

    /// <summary>
    /// 
    /// Called once per frame after all Update methods have been called. It updates the token animations.
    /// 
    /// </summary>
    void Update()
    {
        //if it's time for the next frame...
        if (Time.time - nextFrameTime > (1f / frameRate)) {
            //update all tokens with the next animation frame.
            for (var i = 0; i < tokens.Length; i++)
            {
                //get the token at index i.
                var token = tokens[i];
                //if token is null, it has been disabled and is no longer animated.
                if (token != null)
                {
                    //if the token is not null, update its sprite renderer with the next frame.
                    token._renderer.sprite = token.sprites[token.frame];
                    //set the sprite renderer's sprite to the next frame of the animation.
                    if (token.collected && token.frame == token.sprites.Length - 1)
                    {
                        //if the token has been collected and the frame is the last frame of the animation, disable the token.
                        token.gameObject.SetActive(false);
                        //disable the gameObject so it is no longer animated.
                        tokens[i] = null;
                    }
                    //if the token has been collected, do not increment the frame.
                    else
                    {
                        //if the token has not been collected, increment the frame.
                        token.frame = (token.frame + 1) % token.sprites.Length;
                    }
                }
            }
            //calculate the time of the next frame.
            nextFrameTime += 1f / frameRate;
        }
    }

}