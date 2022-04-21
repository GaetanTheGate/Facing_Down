using UnityEngine;

public class RotationEntity : AbstractEntity
{
    [Range(0.0f, 10.0f)] public float rotationSpeed = 5.0f;

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

        SetRotationTowardGravity();
        ComputeRotation(Time.fixedDeltaTime);
    }

    private void SetRotationTowardGravity()
    {
        rotation = Quaternion.Euler(0.0f, 0.0f, new Velocity(gravity.gravity).SubToAngle(270).getAngle());
    }

    private void ComputeRotation(float deltaTime)
    {
        if (rotationSpeed > 0.05)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * deltaTime);
        else
            transform.rotation = rotation;
    }
}
