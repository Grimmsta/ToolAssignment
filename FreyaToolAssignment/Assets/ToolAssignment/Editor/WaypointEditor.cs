using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class WaypointEditor : EditorWindow
{
    [MenuItem("Tools/WaypointManagerTool")]
    public static void OpenUpManagerWindow()
    {
        GetWindow<WaypointEditor>("Waypoint Manager");
    }

    public List<Vector3> waypointList = new List<Vector3>();
    SerializedObject so;
    SerializedProperty propWaypointSize;
    GameObject aiAgent;
    bool aiAgentIsActive = false;

    [Tooltip("Loops between all points if true, goes from A -> B and B -> A  if false")]
    public bool loop = true;

    private void OnEnable()
    {
        so = new SerializedObject(this);
        propWaypointSize = so.FindProperty("waypointList");
        SceneView.duringSceneGui += DuringSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= DuringSceneGUI;
        for (int i = 0; i < waypointList.Count; i++)
        {
            waypointList.Clear();
        }
        aiAgent = null;
        aiAgentIsActive = false;
    }

    private void OnGUI()
    {
        GUILayout.Label("Waypoint route handler", EditorStyles.boldLabel);

        so.Update();
        EditorGUILayout.PropertyField(propWaypointSize);
        so.ApplyModifiedProperties();

        if (GUILayout.Button("Create waypoint route"))
        {
            if (waypointList.Count != waypointList.Count)
            {
                for (int i = 0; i < waypointList.Count; i++)
                {
                    waypointList.Add(new Vector3());
                }
            }
        }

        GUILayout.Space(7);
        GUILayout.Label("AI Handler", EditorStyles.boldLabel);
        aiAgent = EditorGUILayout.ObjectField("AI Agent: ", aiAgent, typeof(GameObject), true) as GameObject;

        if (GUILayout.Button("Instantiate AI"))
        {
            if (waypointList.Count > 0)
            {
                if (aiAgent == null)
                {
                    EditorUtility.DisplayDialog("AI Handler", "There is no AI prefab chosen, please apply an AI prefab to the AI Agent object field", "OK");
                }
                else if (aiAgentIsActive)
                {
                    EditorUtility.DisplayDialog("AI Handler", "There is already an active agent in the scene", "OK");
                }
                else
                {
                    Instantiate(aiAgent, waypointList.First(), Quaternion.identity);
                    aiAgentIsActive = true;
                }
            }
            else
            {
                EditorUtility.DisplayDialog("AI Handler", "There are no waypoints in the scene, please make sure the Waypoint List has at least one item", "OK");
            }
        }

        if (GUILayout.Button("Delete AI"))
        {
            aiAgent = null;
            aiAgentIsActive = false;
        }
    }

    private void DuringSceneGUI(SceneView view)
    {
        so.Update();
        for (int i = 0; i < propWaypointSize.arraySize; i++)
        {
            SerializedProperty prop = propWaypointSize.GetArrayElementAtIndex(i);
            prop.vector3Value = Handles.PositionHandle(prop.vector3Value, Quaternion.identity);

            if (loop)
            {
                Handles.DrawAAPolyLine(waypointList[i], waypointList[(int)Mathf.Repeat(i + 1, waypointList.Count)]);
            }
            else
            {
                Handles.DrawAAPolyLine(waypointList[i], waypointList[i + 1]);
            }
        }
        so.ApplyModifiedProperties();
    }
}
