/// <summary>
/// This event is triggered when the player character enters a trigger with a VictoryZone component.
/// </summary>
/// <typeparam name="PlayerEnteredVictoryZone"></typeparam>
public class PlayerEnteredVictoryZone : Simulation.Event<PlayerEnteredVictoryZone>
{
    public MonoController controller;
    public override void Execute()
    {
    }
}