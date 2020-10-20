using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    SerializedProperty propWaypoint;
    List<Vector3> indexList = new List<Vector3>();
    List<GameObject> activeAIAgentsList = new List<GameObject>();
    GameObject aiAgent;
    int spawnIndex = 0;

    private void OnEnable()
    {
        propWaypoint = serializedObject.FindProperty("waypointList");
        SceneView.duringSceneGui += DuringSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= DuringSceneGUI;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Waypoint route handler", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(propWaypoint);
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Add waypoint"))
        {
            propWaypoint.InsertArrayElementAtIndex(propWaypoint.arraySize);
            serializedObject.ApplyModifiedProperties();
        }

        if (GUILayout.Button("Clear waypoint route"))
        {
            propWaypoint.ClearArray();
            serializedObject.ApplyModifiedProperties();
        }

        GUILayout.Space(7);
        GUILayout.Label("AI Handler", EditorStyles.boldLabel);
        GUILayout.Space(5);
        GUILayout.Label("AI Spawning", EditorStyles.boldLabel);
        EditorGUILayout.IntField("Number of active agents", activeAIAgentsList.Count);
        aiAgent = EditorGUILayout.ObjectField("AI Agent: ", aiAgent, typeof(GameObject), true) as GameObject;
        spawnIndex = EditorGUILayout.IntField("At Element", spawnIndex);
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();

        if (GUILayout.Button("Instantiate AI"))
        {
            if (propWaypoint.arraySize > 0)
            {
                if (aiAgent == null)
                {
                    EditorUtility.DisplayDialog("AI Handler", "There is no AI prefab chosen, please apply an AI prefab to the AI Agent object field", "OK");
                }
                else if (spawnIndex >= propWaypoint.arraySize || spawnIndex < 0)
                {
                    EditorUtility.DisplayDialog("AI Handler", "Element is out of range, make sure you have the waypoint element in your list", "OK");
                }
                else if (indexList != null && indexList.Contains(propWaypoint.GetArrayElementAtIndex(spawnIndex).vector3Value))
                {
                    EditorUtility.DisplayDialog("AI Handler", "There is already an active agent on this position, check so there aren't two waypoints at the same place. Or clear all active AIs", "OK");
                }
                else
                {
                    Instantiate(aiAgent, propWaypoint.GetArrayElementAtIndex(spawnIndex).vector3Value, Quaternion.identity);
                    indexList.Add(propWaypoint.GetArrayElementAtIndex(spawnIndex).vector3Value);
                    activeAIAgentsList.Add(aiAgent);
                }
            }
            else
            {
                EditorUtility.DisplayDialog("AI Handler", "There are no waypoints in the scene, please make sure the Waypoint List has at least one item", "OK");
            }
        }

        serializedObject.ApplyModifiedProperties();

        GUILayout.Space(10);
        GUILayout.Label("AI De-spawning", EditorStyles.boldLabel);
       
        if (GUILayout.Button("Clear all active AIs"))
        {
            EditorUtility.DisplayDialog("AI Handler", "The AI has been removed, remember to delete them from the hierarchy", "OK");
            activeAIAgentsList.Clear();
            indexList.Clear();
        }

        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
    }

    private void DuringSceneGUI(SceneView view)
    {

        for (int i = 0; i < propWaypoint.arraySize; i++)
        {
            SerializedProperty prop = propWaypoint.GetArrayElementAtIndex(i);
            prop.vector3Value = Handles.PositionHandle(prop.vector3Value, Quaternion.identity);

            Handles.DrawAAPolyLine(propWaypoint.GetArrayElementAtIndex(i).vector3Value, propWaypoint.GetArrayElementAtIndex((int)Mathf.Repeat(i + 1, propWaypoint.arraySize)).vector3Value);

        }
        serializedObject.ApplyModifiedProperties();
    }
}
