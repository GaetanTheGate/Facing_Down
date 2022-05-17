using UnityEngine;

public class RotationEntity : AbstractEntity
{
    [Range(0.0f, 10.0f)] public float rotationSpeed = 3.0f;

    private Quaternion rotation = new Quaternion();
    private GravityEntity gravity;

    private Rigidbody2D rb;

    public override void Init()
    {
        gravity = gameObject.GetComponent<GravityEntity>();
        if (gravity == null)
            gravity = gameObject.AddComponent<GravityEntity>();


        rb = Entity.initRigidBody(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!rb.simulated)
            return;

        //SetRotationTowardVelocity();
        SetRotationTowardGravity();
        ComputeRotation(Time.fixedDeltaTime);
    }

    private void SetRotationTowardGravity()
    {
        rotation = Quaternion.Euler(0.0f, 0.0f, new Velocity(gravity.gravity).SubToAngle(270).getAngle());
    }

    private void SetRotationTowardVelocity()
    {
        FlipEntityRelativeToGravity(new Velocity(rb.velocity).getAngle());
        rotation = Quaternion.Euler(0.0f, 0.0f, ComputeRotationRelativeToFlip(new Velocity(rb.velocity).getAngle()));
    }

    private void ComputeRotation(float deltaTime)
    {
        if (rotationSpeed > 0.05)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * deltaTime);
        else
            transform.rotation = rotation;
    }


    public bool FlipEntityRelativeToGravity(float angle)
    {
        float angleDirection = new Velocity(1, angle).SubToAngle(gravity.gravity.getAngle()).getAngle();
        if (angleDirection > 180 && angleDirection <= 360)
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            return true;
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            return false;
        }
    }

    public float ComputeRotationRelativeToFlip(float angle)
    {
        float angleDirection = angle - (transform.localScale.x < 0 ? 180 : 0);
        return angleDirection;
    }

    public float RotateEntityRelativeToFlip(float angle)
    {
        float angleDirection = ComputeRotationRelativeToFlip(angle);
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angleDirection);
        return angleDirection;
    }
}
