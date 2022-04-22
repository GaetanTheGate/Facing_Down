using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : Attack
{
    private Rigidbody2D rb;

    public Velocity gravity = new Velocity();
    public float speed = 1.0f;
    public List<string> tagsToDestroyOn = new List<string>();

    protected override void ComputeAttack(float percentageTime)
    {
        tagsToDestroyOn.Add("Terrain");

        Velocity grav = new Velocity(gravity);
        grav.MulToSpeed(percentageTime >= 1 ? 1 : 0);
        rb.velocity += grav.GetAsVector2();
    }

    protected override void onStart()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();

        rb.gravityScale = 0;
        rb.velocity = new Velocity(speed, angle).GetAsVector2();
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tag in tagsToDestroyOn)
        {
            if (collision.CompareTag(tag))
                Destroy(gameObject);
        }
    }
}
