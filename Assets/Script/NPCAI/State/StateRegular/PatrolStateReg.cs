
using UnityEngine;

public class PatrolStateReg : StateRegular
{
    protected int currentWaypointIndex;
    protected Transform[] waypoints;
    public PatrolStateReg(AIController controller) : base(controller)
    {
        waypoints = (controller as IRandomPatrol).GetWaypoints();


    }

    public override void EnterState()
    {
        if (agent.isStopped)
        {
            agent.isStopped = false;
        }
        agent.ResetPath();
    }

    public override void ExitState()
    {
        agent.isStopped = true;
        agent.ResetPath();
    }

    public override void UpdateState()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            MoveToNextWaypoint();
        }
    }
    protected void MoveToNextWaypoint()
    {
        agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}