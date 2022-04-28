using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : Attack
{
    public float rotationSpeed = 0f;
    private Rigidbody2D rb;
    private bool hasShot = false;

    public Velocity gravity = new Velocity();
    public float speed = 1.0f;
    public List<string> layersToDestroyOn = new List<string>();

    public int numberOfHitToDestroy = 1;
    private int numberOfHit = 0;

    protected override void ComputeAttack(float percentageTime)
    {
        if (!hasShot && percentageTime != 0)
        {
            rb.velocity = new Velocity(speed, angle).GetAsVector2();
            hasShot = true;
        }
        else if (hasShot)
        {

        }
        else
            return;
        Velocity grav = new Velocity(gravity).MulToSpeed(Time.fixedDeltaTime).MulToSpeed(4.0f);
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( ! gameObject.GetComponent<Collider2D>().IsTouching(collision))
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            Destroy(gameObject);

        foreach (string layer in layersToDestroyOn)
            if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(layer)))
            {
                    ++numberOfHit;
                    if (numberOfHit >= numberOfHitToDestroy)
                        Destroy(gameObject);
            }
    }
}
