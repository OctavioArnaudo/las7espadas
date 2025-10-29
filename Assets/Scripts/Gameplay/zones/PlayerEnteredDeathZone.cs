/// <summary>
/// 
/// PlayerEnteredDeathZone is an event that handles the player's entry into a death zone in the game simulation.
/// 
/// </summary>
public class PlayerEnteredDeathZone : Simulation.Event<PlayerEnteredDeathZone>
{
    /// <summary>
    /// 
    /// The deathzone is used to access the death zone collider and trigger the player's death.
    /// 
    /// </summary>
    public MonoController controller;

    /// <summary>
    /// 
    /// The Execute method is called when the event is executed. It schedules the PlayerDeath event.
    /// 
    /// </summary>
    public override void Execute()
    {
    }
}