using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Min(0)] public float acceleration = 0.5f;
    [Min(0)] public float distance_max = 10.0f;
    [Min(0)] public float distance_min = 1.0f;

    [Min(0.1f)] private float max_min_speed_multiplier = 4f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = target.position + offset;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float dist_x = transform.position.x - target.position.x;
        float dist_y = transform.position.y - target.position.y;
        float dist_z = transform.position.z - target.position.z;

        float distance = Mathf.Sqrt(Mathf.Pow(dist_x, 2) + Mathf.Pow(dist_y, 2) + Mathf.Pow(dist_z, 2));
        
        float howQuick;
        if (distance < distance_max)
            if (distance > distance_min)
                howQuick = acceleration;
            else
                howQuick = acceleration / max_min_speed_multiplier;
        else
            howQuick = acceleration * max_min_speed_multiplier;
        transform.position = Vector3.Lerp(transform.position, target.position + offset, howQuick * Time.deltaTime) ;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void TeleportToTarget()
    {
        transform.position = target.position;
    }
}
