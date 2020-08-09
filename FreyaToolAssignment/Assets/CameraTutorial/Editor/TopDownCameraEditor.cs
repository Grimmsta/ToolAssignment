using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TopDownCamera))]
public class TopDownCameraEditor : Editor
{
    private TopDownCamera targetCamera;
    private Transform camTarget; 
    private void OnEnable()
    {
        targetCamera = (TopDownCamera)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    private void OnSceneGUI()
    {
        if (!targetCamera.m_target)
        {
            return;
        }
        camTarget = targetCamera.m_target;

        Handles.color = new Color(1f, 0f, 0f, 0.15f);
        Handles.DrawSolidDisc(camTarget.position, Vector3.up, targetCamera.m_distance);

        Handles.color = new Color(0f, 1f, 0f, 0.75f);
        Handles.DrawWireDisc(camTarget.position, Vector3.up, targetCamera.m_distance);

        Handles.color = new Color(1f, 0f, 0f, 0.55f);
        targetCamera.m_distance = Handles.ScaleSlider(targetCamera.m_distance, camTarget.position, -camTarget.forward, Quaternion.identity, targetCamera.m_distance, 1f);
        targetCamera.m_distance = Mathf.Clamp(targetCamera.m_distance, 10, float.MaxValue);
        
        Handles.color = new Color(0f, 0f, 1f, 0.55f);
        targetCamera.m_height= Handles.ScaleSlider(targetCamera.m_height, camTarget.position, camTarget.up, Quaternion.identity, targetCamera.m_height, 1f);
        targetCamera.m_height = Mathf.Clamp(targetCamera.m_height, 10f, float.MaxValue);

        GUIStyle labelStyle = new GUIStyle();
        labelStyle.fontSize = 15;
        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.UpperCenter;

        Handles.Label(camTarget.position + (-camTarget.forward * targetCamera.m_distance), "Distance", labelStyle);
        Handles.Label(camTarget.position + (camTarget.up * targetCamera.m_height), "Height", labelStyle);
    }
}
