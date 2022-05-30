using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThrowableAttack : Attack
{
    public Transform followTransform;
    public float followMaxAngle = 10f;

    public float rotationSpeed = 0f;
    protected Rigidbody2D rb;
    protected bool hasShot = false;

    public Velocity gravity = new Velocity(0,0);
    public float speed = 1.0f;

    protected override void ComputeAttack(float percentageTime)
    {
        if (!hasShot && percentageTime != 0)
        {
            rb.velocity = new Velocity(speed, angle).GetAsVector2();
            hasShot = true;
        }
        else if (hasShot)
        {
            Velocity nextVelo = new Velocity(rb.velocity);
            nextVelo.setSpeed(speed * percentageTime);

            if(followTransform != null)
            {
                float angle = Angles.AngleBetweenVector2(transform.position, followTransform.transform.position);

                angle = Mathf.MoveTowardsAngle(nextVelo.getAngle(), angle, followMaxAngle * Time.fixedDeltaTime);
                nextVelo.setAngle(angle);
            }


            rb.velocity = nextVelo.GetAsVector2();

        }
        else
            return;
        Velocity grav = new Velocity(gravity).MulToSpeed(Time.fixedDeltaTime).MulToSpeed(2.0f);
        grav.MulToSpeed(percentageTime >= 1 ? 1 : 0);
        rb.velocity += grav.GetAsVector2();
        transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + rotationSpeed * Time.fixedDeltaTime);
    }

    protected override void onStart()
    {
        rb = Entity.initRigidBody(gameObject);

        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), src.GetComponent<Collider2D>());

        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.velocity = new Vector3();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    protected virtual void Behaviour(float percentageTime) { }
}
