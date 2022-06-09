using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickLaser : MeleeWeapon
{
    public QuickLaser() : this("Enemy") { }
    public QuickLaser(string target) : base(target, "QuickLaser")
    {
        baseAtk = 50;
        baseRange = 50;
        baseLenght = 0.5f;
        baseSpan = 0.025f;
        baseEDelay = 0.1f;
        baseCooldown = 0.1f;

        attackPath = "Prefabs/Items/Weapons/Laser";
        specialPath = "Prefabs/Items/Weapons/Laser";
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject laser = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = GetBaseDmg(self);
        AddHitAttack(laser, new DamageInfo(self, dmg, new Velocity(0.125f * dmg, angle)));

        laser.transform.position = startPos;
        laser.AddComponent<LaserAttack>();

        laser.GetComponent<LaserAttack>().src = self;
        laser.GetComponent<LaserAttack>().angle = angle;
        laser.GetComponent<LaserAttack>().range = baseRange;
        laser.GetComponent<LaserAttack>().lenght = baseLenght;
        laser.GetComponent<LaserAttack>().timeSpan = baseSpan;
        laser.GetComponent<LaserAttack>().endDelay = baseEDelay;

        laser.GetComponent<LaserAttack>().followEntity = false;

        return laser.GetComponent<LaserAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject laser = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);

        float dmg = GetBaseDmg(self);
        AddHitAttack(laser, new DamageInfo(self, dmg * 5, new Velocity(0.5f * dmg, angle)));

        laser.transform.position = startPos;
        laser.AddComponent<LaserAttack>();

        laser.GetComponent<LaserAttack>().src = self;
        laser.GetComponent<LaserAttack>().angle = angle;
        laser.GetComponent<LaserAttack>().range = baseRange;
        laser.GetComponent<LaserAttack>().lenght = baseLenght * 2;
        laser.GetComponent<LaserAttack>().timeSpan = baseSpan * 1.5f;
        laser.GetComponent<LaserAttack>().endDelay = baseEDelay * 1.5f;

        laser.GetComponent<LaserAttack>().followEntity = false;
        return laser.GetComponent<LaserAttack>();
    }

    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).startAttack();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        GetSpecial(angle, self).startAttack();
    }
}
