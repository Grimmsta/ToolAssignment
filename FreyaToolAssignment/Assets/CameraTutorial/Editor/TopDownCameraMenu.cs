using UnityEngine;
using UnityEditor;

public class TopDownCameraMenu : MonoBehaviour
{
    [MenuItem("Tools/Top Down Camera")]
    public static void CreateTopDownCamera()
    {
        GameObject[] selectedGo = Selection.gameObjects;

        if (selectedGo.Length > 0 && selectedGo[0].GetComponent<Camera>())
        {
            if (selectedGo.Length < 2)
            {
                AttachTopDownScript(selectedGo[0].gameObject, null);
            }
            else if (selectedGo.Length == 2)
            {
                AttachTopDownScript(selectedGo[0].gameObject, selectedGo[1].transform);
            }
            else if (selectedGo.Length == 3)
            {
                EditorUtility.DisplayDialog("Camera Tools", " You can only have 2 selected GameObjects for this operation to work, " +
                    "and the first selected GameObject needs to have a Camera component", "OK");
            }
        }
        else
        {
            EditorUtility.DisplayDialog("Camera Tools", "You need to select a Gameobject in the scene that " +
                "has a Camera component assigned to it!", "OK");
        }
    }

    private static void AttachTopDownScript(GameObject aCamera, Transform aTarget)
    {
        TopDownCamera cameraScript = null;

        if (aCamera)
        {
            cameraScript = aCamera.AddComponent<TopDownCamera>();
            if (cameraScript && aTarget)
            {
                cameraScript.m_target = aTarget;
            }

            Selection.activeObject = aCamera;
        }
    }
}
