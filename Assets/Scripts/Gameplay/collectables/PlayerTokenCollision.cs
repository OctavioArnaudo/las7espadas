using UnityEngine;

/// <summary>
/// 
/// PlayerTokenCollision is an event that handles the collision between the player and a token in the game simulation.
/// 
/// </summary>
public class PlayerTokenCollision : Simulation.Event<PlayerTokenCollision>
{
    /// <summary>
    /// 
    /// The player controller is used to access the player's audio source and token collection sound.
    /// 
    /// </summary>
    public MonoController player;
    /// <summary>
    /// 
    /// The token instance represents the token that the player has collected.
    /// 
    /// </summary>
    public TokenInstance token;

    /// <summary>
    /// 
    /// The Execute method is called when the event is executed. It plays the token collection sound and schedules the TokenCollect event.
    /// 
    /// </summary>
    public override void Execute()
    {
        // Check if the player controller is null
        AudioSource.PlayClipAtPoint(token.tokenCollectAudio, token.transform.position);
    }
}