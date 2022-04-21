using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daggers : Weapon
{
    public Daggers()
    {
        isAuto = true;

        baseAtk = 60;
        baseRange = 1;
        baseLenght = 45;
        baseEDelay = 0.05f;
        baseSpan = 0.1f;
        baseCooldown = -0.07f;

        attackPath = "Prefabs/Weapons/Daggers";
        specialPath = "Prefabs/Weapons/Daggers";
    }

    private HalfSlashAttack.Way way = HalfSlashAttack.Way.CounterClockwise;

    public override void Attack(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        swing.transform.position = self.transform.position;
        swing.AddComponent<HalfSlashAttack>();

        swing.GetComponent<HalfSlashAttack>().src = self;
        swing.GetComponent<HalfSlashAttack>().acceleration = 3.0f;
        swing.GetComponent<HalfSlashAttack>().range = baseRange;
        swing.GetComponent<HalfSlashAttack>().lenght = baseLenght;
        swing.GetComponent<HalfSlashAttack>().timeSpan = baseSpan;
        swing.GetComponent<HalfSlashAttack>().endDelay = baseEDelay;
        swing.GetComponent<HalfSlashAttack>().followEntity = false;
        swing.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;
        swing.GetComponent<HalfSlashAttack>().way = way;
        swing.GetComponent<HalfSlashAttack>().angle = angle;

        swing.GetComponent<HalfSlashAttack>().startAttack();


        if (way == HalfSlashAttack.Way.Clockwise)
            way = HalfSlashAttack.Way.CounterClockwise;
        else if (way == HalfSlashAttack.Way.CounterClockwise)
            way = HalfSlashAttack.Way.Clockwise;
    }

    private Vector2 startVelo;
    private int countMax = 10;
    private int count = 0;

    public override void Special(float angle, Entity self)
    {
        startVelo = self.GetComponent<Rigidbody2D>().velocity;

        SpecialAttack(self, angle);
    }

    private void SpecialAttack(Entity self, float angle)
    {
        if(count >= countMax)
        {
            self.GetComponent<Rigidbody2D>().velocity = startVelo;

            GameObject swing1 = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
            swing1.transform.position = self.transform.position;
            swing1.AddComponent<HalfSlashAttack>();

            swing1.GetComponent<HalfSlashAttack>().src = self;
            swing1.GetComponent<HalfSlashAttack>().acceleration = 1.5f;
            swing1.GetComponent<HalfSlashAttack>().range = baseRange * 1.5f;
            swing1.GetComponent<HalfSlashAttack>().lenght = baseLenght * 1.5f;

            swing1.GetComponent<HalfSlashAttack>().startDelay = 0.1f;
            swing1.GetComponent<HalfSlashAttack>().endDelay = 0.3f;
            swing1.GetComponent<HalfSlashAttack>().timeSpan = 0.1f;
            swing1.GetComponent<HalfSlashAttack>().followEntity = false;
            swing1.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;

            GameObject swing2 = GameObject.Instantiate(swing1);

            swing1.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.CounterClockwise;
            swing1.GetComponent<HalfSlashAttack>().angle = angle + 45;
            swing2.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.Clockwise;
            swing2.GetComponent<HalfSlashAttack>().angle = angle - 45;


            swing1.GetComponent<HalfSlashAttack>().onEndAttack += EndSpecialAttack;
            swing1.GetComponent<HalfSlashAttack>().startAttack();
            swing2.GetComponent<HalfSlashAttack>().startAttack();


            return;
        }


        self.GetComponent<Rigidbody2D>().velocity = new Vector3();
        Game.time.SetGameSpeedInstant(0.05f);

        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        swing.transform.position = self.transform.position;
        swing.AddComponent<HalfSlashAttack>();

        swing.GetComponent<HalfSlashAttack>().src = self;
        swing.GetComponent<HalfSlashAttack>().acceleration = 3.0f;
        swing.GetComponent<HalfSlashAttack>().range = baseRange * 0.75f;
        swing.GetComponent<HalfSlashAttack>().lenght = baseLenght * 0.75f;
        swing.GetComponent<HalfSlashAttack>().timeSpan = 0.1f / countMax;
        swing.GetComponent<HalfSlashAttack>().endDelay = 0.05f / countMax;
        swing.GetComponent<HalfSlashAttack>().followEntity = false;
        swing.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;
        swing.GetComponent<HalfSlashAttack>().way = way;
        swing.GetComponent<HalfSlashAttack>().angle = angle;
        swing.GetComponent<HalfSlashAttack>().onEndAttack += SpecialAttack;

        swing.GetComponent<HalfSlashAttack>().startAttack();
        ++count;


        if (way == HalfSlashAttack.Way.Clockwise)
            way = HalfSlashAttack.Way.CounterClockwise;
        else if (way == HalfSlashAttack.Way.CounterClockwise)
            way = HalfSlashAttack.Way.Clockwise;
    }

    private void EndSpecialAttack(Entity self, float angle)
    {
        count = 0;
        Game.time.SetGameSpeedInstant(1.0f);
    }
}
