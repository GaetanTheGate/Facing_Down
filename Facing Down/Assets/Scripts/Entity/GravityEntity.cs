using UnityEngine;

public class GravityEntity : AbstractEntity
{
    private Rigidbody2D rb;

    public Velocity gravity = new Velocity();

    [Range(0.0f, 360.0f)] public float base_gravity_direction = 270;
    [Range(0.0f, 50.0f)] public float base_gravity_speed = 9.8f;

    public override void Init()
    {
        gravity.setAngle(base_gravity_direction);
        gravity.setSpeed(base_gravity_speed);
        base_gravity_direction = gravity.getAngle();

        rb = Entity.initRigidBody(gameObject);

        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ( ! rb.simulated)
            return;

        ComputeGravity(Time.fixedDeltaTime);
    }

    private void ComputeGravity(float deltaTime)
    {
        rb.velocity += new Velocity(gravity).MulToSpeed(deltaTime).MulToSpeed(2).GetAsVector2();
    }
}
