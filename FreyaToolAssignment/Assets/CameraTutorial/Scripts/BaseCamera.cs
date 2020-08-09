using UnityEngine;

public class BaseCamera : MonoBehaviour
{
    public Transform m_target;

    private void Start()
    {
        HandleCamera();
    }

    private void Update()
    {
        HandleCamera();
    }
    protected virtual void HandleCamera()
    {
        if (!m_target)
        {
            return;
        }
    }
}
