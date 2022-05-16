using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daggers : MeleeWeapon
{
    public Daggers() : this("Enemy") { }
    public Daggers(string target) : base(target, "Daggers")
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

    public override void WeaponAttack(float angle, Entity self)
    {
        

        GetAttack(angle, self).startAttack();


        if (way == HalfSlashAttack.Way.Clockwise)
            way = HalfSlashAttack.Way.CounterClockwise;
        else if (way == HalfSlashAttack.Way.CounterClockwise)
            way = HalfSlashAttack.Way.Clockwise;
    }

    private Vector2 startVelo;
    private int countMax = 20;
    private int count = 0;

    public override void WeaponSpecial(float angle, Entity self)
    {
        canAttack = false;

        startVelo = self.GetComponent<Rigidbody2D>().velocity;
        startDelay = baseSpan;
        SpecialAttack(self, angle);
    }

    private float startDelay = 0.0f;

    private void SpecialAttack(Entity self, float angle)
    {
        if(count >= countMax)
        {
            self.GetComponent<Rigidbody2D>().velocity = startVelo;
            
            GetFinalSpecial(angle, self).startAttack();

            return;
        }


        self.GetComponent<Rigidbody2D>().velocity = new Vector3();
        Game.time.SetGameSpeedInstant(0.05f);

        GetSpecial(angle, self).startAttack();
        ++count;

        startDelay = 0.0f;

        if (way == HalfSlashAttack.Way.Clockwise)
            way = HalfSlashAttack.Way.CounterClockwise;
        else if (way == HalfSlashAttack.Way.CounterClockwise)
            way = HalfSlashAttack.Way.Clockwise;
    }

    private void EndSpecialAttack(Entity self, float angle)
    {
        count = 0;
        Game.time.SetGameSpeedInstant(1.0f);
        canAttack = true;
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(swing, new DamageInfo(self, baseAtk * dmg, new Velocity(0.25f * dmg, angle)));

        swing.transform.position = startPos;
        swing.AddComponent<HalfSlashAttack>();

        swing.GetComponent<HalfSlashAttack>().src = self;
        swing.GetComponent<HalfSlashAttack>().acceleration = 3.0f;
        swing.GetComponent<HalfSlashAttack>().range = baseRange;
        swing.GetComponent<HalfSlashAttack>().lenght = baseLenght;
        swing.GetComponent<HalfSlashAttack>().timeSpan = baseSpan;
        swing.GetComponent<HalfSlashAttack>().endDelay = baseEDelay;
        swing.GetComponent<HalfSlashAttack>().followEntity = forceUnFollow;
        swing.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;
        swing.GetComponent<HalfSlashAttack>().way = way;
        swing.GetComponent<HalfSlashAttack>().angle = angle;

        return swing.GetComponent<HalfSlashAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(swing, new DamageInfo(self, baseAtk * dmg * 0.5f, new Velocity(0.125f * dmg, angle)));

        swing.transform.position = startPos;
        swing.AddComponent<HalfSlashAttack>();

        swing.GetComponent<HalfSlashAttack>().src = self;
        swing.GetComponent<HalfSlashAttack>().acceleration = 3.0f;
        swing.GetComponent<HalfSlashAttack>().range = baseRange * 0.75f;
        swing.GetComponent<HalfSlashAttack>().lenght = baseLenght * 0.75f;
        swing.GetComponent<HalfSlashAttack>().timeSpan = 0.1f / countMax;
        swing.GetComponent<HalfSlashAttack>().startDelay = startDelay;
        swing.GetComponent<HalfSlashAttack>().endDelay = 0.05f / countMax;
        swing.GetComponent<HalfSlashAttack>().followEntity = false;
        swing.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;
        swing.GetComponent<HalfSlashAttack>().way = way;
        swing.GetComponent<HalfSlashAttack>().angle = angle;
        swing.GetComponent<HalfSlashAttack>().onEndAttack += SpecialAttack;

        return swing.GetComponent<HalfSlashAttack>();
    }

    private Attack GetFinalSpecial(float angle, Entity self)
    {
        GameObject swing1 = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo dmgInfo = new DamageInfo(self, baseAtk * dmg * 2, new Velocity(0.5f * dmg, angle));
        AddHitAttack(swing1, dmgInfo);

        swing1.transform.position = startPos;
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
        swing2.GetComponent<AttackHit>().dmgInfo = dmgInfo;

        swing1.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.CounterClockwise;
        swing1.GetComponent<HalfSlashAttack>().angle = angle + 45;
        swing2.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.Clockwise;
        swing2.GetComponent<HalfSlashAttack>().angle = angle - 45;


        swing1.GetComponent<HalfSlashAttack>().onEndAttack += EndSpecialAttack;

        GameObject attack = new GameObject();
        attack.AddComponent<CompositeAttack>();
        attack.GetComponent<CompositeAttack>().attackList.Add(swing1.GetComponent<HalfSlashAttack>());
        attack.GetComponent<CompositeAttack>().attackList.Add(swing2.GetComponent<HalfSlashAttack>());

        return attack.GetComponent<CompositeAttack>();
    }
}
