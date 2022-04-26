using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : ProjectileWeapon
{
    public Shuriken(string target) : base(target)
    {
        baseAtk = 50f;
        baseSpeed = 25.0f;
        baseSpan = 0.2f;
        baseEDelay = 5.0f;
        baseCooldown = -baseEDelay + (baseSpan / 2);

        attackPath = "Prefabs/Weapons/Shuriken";
        specialPath = "Prefabs/Weapons/Shuriken";
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject bullet = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        bullet.AddComponent<ProjectileAttack>();
        bullet.transform.position = startPos;

        bullet.GetComponent<ProjectileAttack>().src = self;

        bullet.GetComponent<ProjectileAttack>().angle = angle;
        bullet.GetComponent<ProjectileAttack>().acceleration = 1.0f;
        bullet.GetComponent<ProjectileAttack>().timeSpan = baseSpan;
        bullet.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        bullet.GetComponent<ProjectileAttack>().speed = baseSpeed;
        bullet.GetComponent<ProjectileAttack>().gravity = self.GetComponent<GravityEntity>().gravity;


        AddHitAttack(bullet, baseAtk);
        return bullet.GetComponent<ProjectileAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        throw new System.NotImplementedException();
    }

    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).startAttack();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        throw new System.NotImplementedException();
    }
}
