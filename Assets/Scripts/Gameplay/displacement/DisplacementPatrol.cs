using UnityEngine;

public class DisplacementPatrol : MonoController
{
    public Transform[] patrolPoints;
    private int currentPoint = 0;
    public float patrolSpeed = 3.5f;

    protected override void Start()
    {
        base.Start();
        agent.speed = patrolSpeed;
        NextPoint();
    }

    protected override void Update()
    {
        base.Update();
        if (isPatrolling && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            isPatrolling = false;
            isInPursuit = true;
            agent.speed = patrolSpeed;
            NextPoint();
        }
    }

    public void NextPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.destination = patrolPoints[currentPoint].position;
        currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }
}
