using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarAxe : MeleeWeapon
{
    public WarAxe(string target) : base(target, "WarAxe")
    {
        baseAtk = 300;
        baseRange = 3.5f;
        baseLenght = 225;
        baseSpan = 0.6f;
        baseSDelay = 0.3f;
        baseEDelay = 0.1f;
        baseCooldown = 0.1f;

        attackPath = "Prefabs/Weapons/WarAxe";
        specialPath = "Prefabs/Weapons/WarAxe";
    }

    private SwingAttack.Way way = SwingAttack.Way.CounterClockwise;

    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).GetComponent<SwingAttack>().startAttack();


        if (way == SwingAttack.Way.Clockwise)
            way = SwingAttack.Way.CounterClockwise;
        else if (way == SwingAttack.Way.CounterClockwise)
            way = SwingAttack.Way.Clockwise;
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        canAttack = false;

        GetSpecial(angle, self).startAttack();
    }

    private void nextSpin(Entity self, float angle)
    {
        if (self.GetComponent<EntityCollisionStructure>().isGrounded)
        {
            canAttack = true;
            return;
        }

        self.transform.rotation = Quaternion.Euler(0, 0, angle);

        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(swing, new DamageInfo(self, baseAtk * dmg * 0.5f, new Velocity(0.5f * dmg, angle)));

        swing.transform.position = startPos;
        swing.AddComponent<SwingAttack>();

        swing.GetComponent<SwingAttack>().src = self;
        swing.GetComponent<SwingAttack>().acceleration = 1f;
        swing.GetComponent<SwingAttack>().angle = angle - 10;
        swing.GetComponent<SwingAttack>().range = baseRange;
        swing.GetComponent<SwingAttack>().lenght = 10;
        swing.GetComponent<SwingAttack>().timeSpan = 0.01f;
        swing.GetComponent<SwingAttack>().followEntity = forceUnFollow;

        swing.GetComponent<SwingAttack>().way = SwingAttack.Way.Clockwise;

        swing.GetComponent<SwingAttack>().onEndAttack += nextSpin;
        swing.GetComponent<SwingAttack>().startAttack();
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(swing, new DamageInfo(self, baseAtk * dmg, new Velocity(5 * dmg, angle)));

        swing.transform.position = startPos;
        swing.AddComponent<SwingAttack>();

        swing.GetComponent<SwingAttack>().src = self;
        swing.GetComponent<SwingAttack>().acceleration = 0.7f;
        swing.GetComponent<SwingAttack>().angle = angle;
        swing.GetComponent<SwingAttack>().range = baseRange;
        swing.GetComponent<SwingAttack>().lenght = baseLenght;
        swing.GetComponent<SwingAttack>().timeSpan = baseSpan;
        swing.GetComponent<SwingAttack>().followEntity = forceUnFollow;
        swing.GetComponent<SwingAttack>().startDelay = baseSDelay;
        swing.GetComponent<SwingAttack>().endDelay = baseEDelay;

        swing.GetComponent<SwingAttack>().way = way;

        return swing.GetComponent<SwingAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(swing, new DamageInfo(self, baseAtk * dmg * 2, new Velocity(5 * dmg, angle)));

        swing.transform.position = startPos;
        swing.AddComponent<SwingAttack>();

        swing.GetComponent<SwingAttack>().src = self;
        swing.GetComponent<SwingAttack>().acceleration = 0.6f;
        swing.GetComponent<SwingAttack>().angle = 90;
        swing.GetComponent<SwingAttack>().range = baseRange;
        swing.GetComponent<SwingAttack>().lenght = 0;
        swing.GetComponent<SwingAttack>().timeSpan = 0.0f;
        swing.GetComponent<SwingAttack>().startDelay = 1.0f;
        swing.GetComponent<SwingAttack>().followEntity = forceUnFollow;

        swing.GetComponent<SwingAttack>().way = SwingAttack.Way.Clockwise;

        swing.GetComponent<SwingAttack>().onEndAttack += nextSpin;

        return swing.GetComponent<SwingAttack>();
    }
}
