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
        GameObject shuriken = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        shuriken.AddComponent<ProjectileAttack>();
        shuriken.transform.position = startPos;

        shuriken.GetComponent<ProjectileAttack>().src = self;

        shuriken.GetComponent<ProjectileAttack>().angle = angle;
        shuriken.GetComponent<ProjectileAttack>().acceleration = 1.0f;
        shuriken.GetComponent<ProjectileAttack>().timeSpan = baseSpan;
        shuriken.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        shuriken.GetComponent<ProjectileAttack>().speed = baseSpeed;
        shuriken.GetComponent<ProjectileAttack>().gravity = self.GetComponent<GravityEntity>().gravity;


        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(shuriken, new DamageInfo(self, baseAtk * dmg, new Velocity(1.0f * dmg, angle)));
        return shuriken.GetComponent<ProjectileAttack>();
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
