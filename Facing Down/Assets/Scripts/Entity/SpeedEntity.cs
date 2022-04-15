using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEntity : AbstractEntity
{
    private Rigidbody2D rb;
    private StatEntity stat;

    public override void Init()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        stat = gameObject.GetComponent<StatEntity>();
        if (stat == null)
            stat = gameObject.AddComponent<StatEntity>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckMaxSpeedLimit();
    }

    private void CheckMaxSpeedLimit()
    {/*
        if (self.velocity.getSpeed() > stat.maxSpeed)
        {
            self.velocity.setSpeed(stat.maxSpeed);
        }
        */
        Velocity selfVelo = new Velocity(rb.velocity);
        if (selfVelo.getSpeed() > stat.maxSpeed)
        {
            selfVelo.setSpeed(stat.maxSpeed);
            rb.velocity = selfVelo.GetAsVector2();
        }

    }
}
