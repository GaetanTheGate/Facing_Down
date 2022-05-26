using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MeleeWeapon
{
    public Fist() : this("Enemy") { }
    public Fist(string target) : base(target, "Daggers")
    {
        baseAtk = 100;
        baseRange = 1f;
        baseLenght = 45;
        baseEDelay = 0.025f;
        baseSpan = 0.05f;
        baseSDelay = 0.025f;
        baseCooldown = -(baseEDelay + baseSpan + baseSDelay)/2;

        attackPath = "Prefabs/Weapons/Fist";
        specialPath = "Prefabs/Weapons/Fist";
    }

    private HalfSlashAttack.Way way = HalfSlashAttack.Way.CounterClockwise;

    public override void WeaponAttack(float angle, Entity self)
    {
        float relativeAngle = angle;

        if (way == HalfSlashAttack.Way.Clockwise)
        {
            relativeAngle -= 20;
            way = HalfSlashAttack.Way.CounterClockwise;
        }
        else if (way == HalfSlashAttack.Way.CounterClockwise)
        {
            relativeAngle += 20;
            way = HalfSlashAttack.Way.Clockwise;
        }

        GetAttack(relativeAngle, self).startAttack();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        float relativeAngle = angle;

        if (way == HalfSlashAttack.Way.Clockwise)
        {
            relativeAngle -= 20;
            way = HalfSlashAttack.Way.CounterClockwise;
        }
        else if (way == HalfSlashAttack.Way.CounterClockwise)
        {
            relativeAngle += 20;
            way = HalfSlashAttack.Way.Clockwise;
        }

        GetSpecial(relativeAngle, self).startAttack();
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject punch = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        AddHitAttack(punch, new DamageInfo(self, GetBaseDmg(self), new Velocity(GetKnockbackIntensity(self, 2), angle), baseSDelay + baseSpan + baseEDelay));

        punch.transform.position = startPos;
        punch.AddComponent<HalfSlashAttack>();

        punch.GetComponent<HalfSlashAttack>().src = self;
        punch.GetComponent<HalfSlashAttack>().acceleration = 3.0f;
        punch.GetComponent<HalfSlashAttack>().range = baseRange;
        punch.GetComponent<HalfSlashAttack>().lenght = baseLenght;
        punch.GetComponent<HalfSlashAttack>().startDelay = baseSDelay;
        punch.GetComponent<HalfSlashAttack>().timeSpan = baseSpan;
        punch.GetComponent<HalfSlashAttack>().endDelay = baseEDelay;
        punch.GetComponent<HalfSlashAttack>().followEntity = forceUnFollow;
        punch.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;
        punch.GetComponent<HalfSlashAttack>().way = way;
        punch.GetComponent<HalfSlashAttack>().angle = angle;

        return punch.GetComponent<HalfSlashAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject punch = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        AddHitAttack(punch, new DamageInfo(self, GetBaseDmg(self) * 2, new Velocity(GetKnockbackIntensity(self, 4), angle), baseSDelay * 3 + baseSpan * 2 + baseEDelay * 4));

        punch.transform.position = startPos;
        punch.AddComponent<HalfSlashAttack>();

        punch.GetComponent<HalfSlashAttack>().src = self;
        punch.GetComponent<HalfSlashAttack>().acceleration = 3.0f;
        punch.GetComponent<HalfSlashAttack>().range = baseRange * 2;
        punch.GetComponent<HalfSlashAttack>().lenght = baseLenght;
        punch.GetComponent<HalfSlashAttack>().startDelay = baseSDelay * 3;
        punch.GetComponent<HalfSlashAttack>().timeSpan = baseSpan * 2;
        punch.GetComponent<HalfSlashAttack>().endDelay = baseEDelay * 4;
        punch.GetComponent<HalfSlashAttack>().followEntity = forceUnFollow;
        punch.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;
        punch.GetComponent<HalfSlashAttack>().way = way;
        punch.GetComponent<HalfSlashAttack>().angle = angle;

        return punch.GetComponent<HalfSlashAttack>();
    }

    public override void _Move(float angle, Entity self)
    {

        self.GetComponent<Rigidbody2D>().velocity += new Velocity(self.GetComponent<GravityEntity>().gravity).SubToAngle(180).setSpeed(10).GetAsVector2();
    }
}
