using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> waypointList;
    public List<Vector3> WaypointList => waypointList;
}