using UnityEngine;

public class Waypoint : MonoBehaviour
{
    //Contain the value for the waypoint

    private Vector3 pos;
    private bool hasBeenVisited = false;

    //OnValidate: update the pos
    
    //Draw a handle for the waypont


    //MIght delete this below:
    private void OnEnable()
    {
        WaypointManager.waypoinList.Add(this);
    }

    private void OnDisable()
    {
        WaypointManager.waypoinList.Remove(this);
    }
}
