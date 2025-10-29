public class MoveInPursuit : MoveInPatrol
{
    public float pursuitRange = 15f;
    public float pursuitSpeed = 6f;

    protected override void Update()
    {
        base.Update();
        if (isInPursuit && spawnDistance > pursuitRange)
        {
            isPatrolling = true;
            isInPursuit = false;
            agent.speed = pursuitSpeed;
            NextPoint();
        }
    }
}