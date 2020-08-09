using UnityEngine;

public class TopDownCamera : BaseCamera
{
    public float m_height = 10f;
    public float m_distance = 20f;
    public float m_angle = 45f;
    
    public float smoothSpeed = .5f;

    private Vector3 refVelocity;
    private Vector3 worldPos;
    private Vector3 rotatedVector;
    private Vector3 flatTargetPos;
    private Vector3 finalPosition;

    protected override void HandleCamera()
    {
        base.HandleCamera();

        worldPos = (Vector3.forward * -m_distance) + (Vector3.up * m_height);
        //Debug.DrawLine(target.position, worldPos, Color.red);

        rotatedVector = Quaternion.AngleAxis(m_angle, Vector3.up) * worldPos;
        //Debug.DrawLine(target.position, rotatedVector, Color.green);

        flatTargetPos = m_target.position;
        flatTargetPos.y = 0f;

        finalPosition = flatTargetPos + rotatedVector;
        //Debug.DrawLine(target.position, finalPosition, Color.blue);

        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref refVelocity, smoothSpeed) ;
        transform.LookAt(flatTargetPos);
    }
}
