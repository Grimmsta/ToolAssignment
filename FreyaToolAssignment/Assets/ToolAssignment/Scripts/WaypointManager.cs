using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaypointManager : EditorWindow
{
    [MenuItem("Tools/WaypointManagerTool")]
    public static void OpenUpManagerWindow()
    {
        GetWindow<WaypointManager>("Waypoint Manager");
    }
    //Do the logic for the waypoint system, have the AI to use this

    public static List <Waypoint> waypoinList = new List<Waypoint>();
    SerializedObject so;

    private void OnEnable()
    {
        so = new SerializedObject(this);
    }

    private void OnGUI()
    {
        GUILayout.Label("Waypoint List:", EditorStyles.boldLabel);
        if(GUILayout.Button("Create Waypoint Manager"))
        {
            Debug.Log("created a waypoint!");
        }
    }

    private void DuringSceneGUI()
    {

    }

    //Draw line between points

    //Button to create a new waypoint
}
