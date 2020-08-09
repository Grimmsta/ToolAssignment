using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaypointManager))]
public class WaypointEditor : MonoBehaviour
{
    //Draw the visuals for the waypoint
    public List<Vector3> waypointList = new List<Vector3>();
    
    [Tooltip("Loops between all points if true, goes from A -> B and B -> A  if false")]
    public bool loop = true;
    
    private void OnDrawGizmosSelected()
    {
        foreach (Vector3 item in waypointList)
        {
             Handles.PositionHandle(item, Quaternion.identity);
        }

        for (int i = 0; i < waypointList.Count; i++)
        {
            if (loop)
            {
                Handles.DrawAAPolyLine(waypointList[i], waypointList[(int)Mathf.Repeat(i + 1, waypointList.Count)]);
            }
            else
            {
                Handles.DrawAAPolyLine(waypointList[i], waypointList[i+1]);
            }
        }
    }
}
