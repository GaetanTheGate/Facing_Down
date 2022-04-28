using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : Attack
{
    private Rigidbody2D rb;
    private bool hasShot = false;

    public Velocity gravity = new Velocity();
    public float speed = 1.0f;
    public List<string> tagsToDestroyOn = new List<string>();

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
        transform.localRotation = Quaternion.Euler(0, 0, new Velocity(rb.velocity).getAngle());
    }

    protected override void onStart()
    {
        tagsToDestroyOn.Add("Terrain");
        rb = gameObject.AddComponent<Rigidbody2D>();

        rb.gravityScale = 0;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        rb.velocity = new Vector3();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tag in tagsToDestroyOn)
        {
            if (collision.CompareTag(tag))
                if (gameObject.GetComponent<Collider2D>().IsTouching(collision)) 
                    Destroy(gameObject);
        }
    }
}
