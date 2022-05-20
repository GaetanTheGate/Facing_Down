using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEntity : AbstractPlayer
{
    private Rigidbody2D rb;
    private StatPlayer stat;

    protected override void Initialize()
    {
        rb = gameObject.GetComponent<Player>().self.gameObject.GetComponent<Rigidbody2D>();
        stat = gameObject.GetComponent<Player>().stat;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckMaxSpeedLimit();
    }

    private void CheckMaxSpeedLimit()
    {
        Velocity selfVelo = new Velocity(rb.velocity);
        if (selfVelo.getSpeed() > stat.maxSpeed)
        {
            selfVelo.setSpeed(stat.maxSpeed);
            rb.velocity = selfVelo.GetAsVector2();
        }

    }
}
