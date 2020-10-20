using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private Waypoint waypoint;
    public float speed;
    private int currentPathIndex = 0;

    void Start()
    {
        waypoint = FindObjectOfType<Waypoint>();
    }

    void Update()
    {
        Vector3 target = waypoint.WaypointList[currentPathIndex];
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        if (Equals(target, transform.position))
        {
            currentPathIndex = (currentPathIndex + 1) % waypoint.WaypointList.Count;
        }
    }
}
