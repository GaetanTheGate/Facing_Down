using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEntity : MonoBehaviour
{
    private Rigidbody2D rb;
    private Entity self;
    private StatEntity stat;

    public void Init()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        self = gameObject.GetComponent<Entity>();
        if (self == null)
            self = gameObject.AddComponent<Entity>();

        stat = gameObject.GetComponent<StatEntity>();
        if (stat == null)
            stat = gameObject.AddComponent<StatEntity>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
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
