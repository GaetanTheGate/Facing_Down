using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : Weapon
{
    public Wings()
    {

        baseAtk = 40;
        baseRange = 2;
        baseLenght = 360;
        baseSpan = 0.4f;
        baseCooldown = 0.5f;

        attackPath = "Prefabs/Weapons/Katana";
        specialPath = "Prefabs/Weapons/Katana";
    }


    public override void Attack(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        swing.transform.position = self.transform.position;
        swing.AddComponent<SlashAttack>();

        swing.GetComponent<SlashAttack>().src = self;
        swing.GetComponent<SlashAttack>().range = baseRange;
        swing.GetComponent<SlashAttack>().lenght = baseLenght;
        swing.GetComponent<SlashAttack>().timeSpan = baseSpan;
        swing.GetComponent<SlashAttack>().followEntity = true;

        GameObject swing2 = GameObject.Instantiate(swing);


        swing.GetComponent<SlashAttack>().angle = angle + 45;
        swing.GetComponent<SlashAttack>().way = SlashAttack.Way.Clockwise;

        swing2.GetComponent<SlashAttack>().angle = angle - 45;
        swing2.GetComponent<SlashAttack>().way = SlashAttack.Way.CounterClockwise;

        swing.GetComponent<SlashAttack>().startAttack();
        swing2.GetComponent<SlashAttack>().startAttack();

        Velocity newVelo = new Velocity(baseAtk / 8, angle + 180);
        self.GetComponent<Rigidbody2D>().velocity += newVelo.GetAsVector2();
    }

    public override void Special(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);
        swing.transform.position = self.transform.position;
        swing.AddComponent<SlashAttack>();

        swing.GetComponent<SlashAttack>().src = self;
        swing.GetComponent<SlashAttack>().range = baseRange * 3;
        swing.GetComponent<SlashAttack>().lenght = baseLenght;
        swing.GetComponent<SlashAttack>().timeSpan = baseSpan * 4;
        swing.GetComponent<SlashAttack>().followEntity = false;

        GameObject swing2 = GameObject.Instantiate(swing);


        swing.GetComponent<SlashAttack>().angle = angle + 45;
        swing.GetComponent<SlashAttack>().way = SlashAttack.Way.Clockwise;

        swing2.GetComponent<SlashAttack>().angle = angle - 45;
        swing2.GetComponent<SlashAttack>().way = SlashAttack.Way.CounterClockwise;

        swing.GetComponent<SlashAttack>().startAttack();
        swing2.GetComponent<SlashAttack>().startAttack();

        Velocity newVelo = new Velocity(baseAtk / 4, angle + 180);
        self.GetComponent<Rigidbody2D>().velocity += newVelo.GetAsVector2();
    }
}
