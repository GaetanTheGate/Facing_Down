using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : ProjectileWeapon
{
    public Grenade() : this("Enemy") { }
    public Grenade(string target) : base(target, "Grenade")
    {
        attackWeapon = new Explosion(target);
        specialWeapon = new Explosion(target);

        baseSDelay = 0.0f;
        baseSpan = 0.0f;
        baseEDelay = 3.0f;
        baseCooldown = -2.0f;

        baseSpeed = 15f;

        attackPath = "Prefabs/Items/Weapons/Grenade";
        specialPath = "Prefabs/Items/Weapons/Grenade";
    }

    private Weapon attackWeapon;
    private Weapon specialWeapon;

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject grenade = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        grenade.AddComponent<GrenadeAttack>();
        grenade.GetComponent<GrenadeAttack>().transform.position = startPos;

        grenade.GetComponent<GrenadeAttack>().src = self;
        grenade.GetComponent<GrenadeAttack>().gravity = self.GetComponent<GravityEntity>().gravity;
        grenade.GetComponent<GrenadeAttack>().startDelay = baseSDelay;
        grenade.GetComponent<GrenadeAttack>().timeSpan = baseSpan;
        grenade.GetComponent<GrenadeAttack>().endDelay = baseEDelay;
        grenade.GetComponent<GrenadeAttack>().weapon = attackWeapon;
        grenade.GetComponent<GrenadeAttack>().speed = baseSpeed;
        grenade.GetComponent<GrenadeAttack>().angle = angle;

        return grenade.GetComponent<GrenadeAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject grenade = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);
        grenade.AddComponent<GrenadeAttack>();
        grenade.GetComponent<GrenadeAttack>().transform.position = startPos;

        grenade.GetComponent<GrenadeAttack>().src = self;
        grenade.GetComponent<GrenadeAttack>().gravity = self.GetComponent<GravityEntity>().gravity;
        grenade.GetComponent<GrenadeAttack>().startDelay = baseSDelay;
        grenade.GetComponent<GrenadeAttack>().timeSpan = baseSpan;
        grenade.GetComponent<GrenadeAttack>().endDelay = baseEDelay;
        grenade.GetComponent<GrenadeAttack>().weapon = specialWeapon;
        grenade.GetComponent<GrenadeAttack>().isSpecial = true;
        grenade.GetComponent<GrenadeAttack>().speed = baseSpeed * 1.25f;
        grenade.GetComponent<GrenadeAttack>().angle = angle;

        return grenade.GetComponent<GrenadeAttack>();
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
