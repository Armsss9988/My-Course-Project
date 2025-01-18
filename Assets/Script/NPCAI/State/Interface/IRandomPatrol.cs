using UnityEngine;

public interface IRandomPatrol
{
    public Transform[] GetWaypoints();
    public int GetCurrentWayPointIndex();
    public void SetCurrentWayPointIndex(int waypointIndex);

}
