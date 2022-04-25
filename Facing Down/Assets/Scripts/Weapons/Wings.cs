using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : MeleeWeapon
{
    private float difference = 20.0f;
    public Wings(string target) : base(target)
    {

        baseAtk = 40;
        baseRange = 2;
        baseLenght = 180;
        baseSpan = 0.3f;
        baseCooldown = 0.1f;

        attackPath = "Prefabs/Weapons/Wings";
        specialPath = "Prefabs/Weapons/Wings";
    }


    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).startAttack();
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        swing.transform.position = startPos;
        swing.AddComponent<HalfSlashAttack>();

        swing.GetComponent<HalfSlashAttack>().src = self;
        swing.GetComponent<HalfSlashAttack>().endDelay = baseEDelay;
        swing.GetComponent<HalfSlashAttack>().range = baseRange;
        swing.GetComponent<HalfSlashAttack>().lenght = baseLenght - difference;
        swing.GetComponent<HalfSlashAttack>().timeSpan = baseSpan;
        swing.GetComponent<HalfSlashAttack>().followEntity = forceUnFollow;
        swing.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;
        AddHitAttack(swing, baseAtk);

        GameObject swing2 = GameObject.Instantiate(swing);
        swing.GetComponent<HalfSlashAttack>().onEndAttack += onEndAttack;


        swing.GetComponent<HalfSlashAttack>().angle = angle - difference;
        swing.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.Clockwise;

        swing2.GetComponent<HalfSlashAttack>().angle = angle + difference;
        swing2.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.CounterClockwise;

        GameObject attack = new GameObject();
        attack.AddComponent<CompositeAttack>();
        attack.GetComponent<CompositeAttack>().attackList.Add(swing.GetComponent<HalfSlashAttack>());
        attack.GetComponent<CompositeAttack>().attackList.Add(swing2.GetComponent<HalfSlashAttack>());

        return attack.GetComponent<CompositeAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);
        swing.transform.position = startPos;
        swing.AddComponent<HalfSlashAttack>();

        swing.GetComponent<HalfSlashAttack>().src = self;
        swing.GetComponent<HalfSlashAttack>().endDelay = baseEDelay * 2;
        swing.GetComponent<HalfSlashAttack>().range = baseRange * 3;
        swing.GetComponent<HalfSlashAttack>().lenght = baseLenght - difference;
        swing.GetComponent<HalfSlashAttack>().timeSpan = baseSpan * 2;
        swing.GetComponent<HalfSlashAttack>().followEntity = forceUnFollow;
        swing.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;
        AddHitAttack(swing, baseAtk);

        GameObject swing2 = GameObject.Instantiate(swing);
        swing.GetComponent<HalfSlashAttack>().onEndAttack += onEndSpecial;


        swing.GetComponent<HalfSlashAttack>().angle = angle - difference;
        swing.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.Clockwise;

        swing2.GetComponent<HalfSlashAttack>().angle = angle + difference;
        swing2.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.CounterClockwise;


        GameObject attack = new GameObject();
        attack.AddComponent<CompositeAttack>();
        attack.GetComponent<CompositeAttack>().attackList.Add(swing.GetComponent<HalfSlashAttack>());
        attack.GetComponent<CompositeAttack>().attackList.Add(swing2.GetComponent<HalfSlashAttack>());

        return attack.GetComponent<CompositeAttack>();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        canAttack = false;

        GetSpecial(angle, self).startAttack();
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

        canAttack = true;
    }
}
