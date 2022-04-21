using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : Weapon
{
    private float difference = 20.0f;
    public Wings()
    {

        baseAtk = 40;
        baseRange = 2;
        baseLenght = 180;
        baseSpan = 0.3f;
        baseCooldown = 0.1f;

        attackPath = "Prefabs/Weapons/Wings";
        specialPath = "Prefabs/Weapons/Wings";
    }


    public override void Attack(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        swing.transform.position = self.transform.position;
        swing.AddComponent<HalfSlashAttack>();

        swing.GetComponent<HalfSlashAttack>().src = self;
        swing.GetComponent<HalfSlashAttack>().endDelay = baseEDelay;
        swing.GetComponent<HalfSlashAttack>().range = baseRange;
        swing.GetComponent<HalfSlashAttack>().lenght = baseLenght - difference;
        swing.GetComponent<HalfSlashAttack>().timeSpan = baseSpan;
        swing.GetComponent<HalfSlashAttack>().followEntity = true;
        swing.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;

        GameObject swing2 = GameObject.Instantiate(swing);
        swing.GetComponent<HalfSlashAttack>().onEndAttack += onEndAttack;


        swing.GetComponent<HalfSlashAttack>().angle = angle - difference;
        swing.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.Clockwise;

        swing2.GetComponent<HalfSlashAttack>().angle = angle + difference;
        swing2.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.CounterClockwise;

        swing.GetComponent<HalfSlashAttack>().startAttack();
        swing2.GetComponent<HalfSlashAttack>().startAttack();
    }

    public override void Special(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);
        swing.transform.position = self.transform.position;
        swing.AddComponent<HalfSlashAttack>();

        swing.GetComponent<HalfSlashAttack>().src = self;
        swing.GetComponent<HalfSlashAttack>().endDelay = baseEDelay * 2;
        swing.GetComponent<HalfSlashAttack>().range = baseRange * 3;
        swing.GetComponent<HalfSlashAttack>().lenght = baseLenght - difference;
        swing.GetComponent<HalfSlashAttack>().timeSpan = baseSpan * 2;
        swing.GetComponent<HalfSlashAttack>().followEntity = true;
        swing.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;

        GameObject swing2 = GameObject.Instantiate(swing);
        swing.GetComponent<HalfSlashAttack>().onEndAttack += onEndSpecial;


        swing.GetComponent<HalfSlashAttack>().angle = angle - difference;
        swing.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.Clockwise;

        swing2.GetComponent<HalfSlashAttack>().angle = angle + difference;
        swing2.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.CounterClockwise;

        swing.GetComponent<HalfSlashAttack>().startAttack();
        swing2.GetComponent<HalfSlashAttack>().startAttack();
    }


    private void onEndAttack(Entity self, float angle)
    {
        Velocity newVelo = new Velocity(baseAtk / 6, angle + 180 + difference);
        self.GetComponent<Rigidbody2D>().velocity += newVelo.GetAsVector2();
    }

    private void onEndSpecial(Entity self, float angle)
    {
        Debug.Log(angle);
        Velocity newVelo = new Velocity(baseAtk / 3, angle + 180 + difference);
        self.GetComponent<Rigidbody2D>().velocity += newVelo.GetAsVector2();
    }
}
