/// <summary>
/// 
/// Represents the different states of a jump in a platformer game.
/// 
/// This enum defines the various stages a character can go through during a jump,
/// 
/// including being grounded, preparing to jump, jumping, in flight, and landed.
/// 
/// </summary>
public enum JumpModel
{
    /// <summary>
    /// 
    /// The character is on the ground and can initiate a jump.
    /// 
    /// </summary>
    IsGrounded,
    /// <summary>
    /// 
    /// The character is preparing to jump, typically by pressing the jump button.
    /// 
    /// </summary>
    PrepareToJump,
    /// <summary>
    /// 
    /// The character is currently in the process of jumping.
    /// 
    /// </summary>
    Jumping,
    /// <summary>
    /// 
    /// The character is in the air after jumping, before landing.
    ///
    /// </summary>
    InFlight,
    /// <summary>
    /// 
    /// The character has landed after a jump.
    /// 
    /// </summary>
    Landed
}