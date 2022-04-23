using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionBreaker : Weapon
{
    public DimensionBreaker()
    {
        baseAtk = 10;
        baseRange = 30;
        baseSDelay = attackWeapon.getSDelay();
        baseSpan = attackWeapon.getSpan();
        baseEDelay = attackWeapon.getEDelay();
        baseCooldown = attackWeapon.getBaseCooldown();

        isAuto = true;

        attackPath = "Prefabs/Weapons/Gun";
        specialPath = "Prefabs/Weapons/Gun";
    }

    private Weapon attackWeapon = EnumWeapon.getRandomWeapon();
    private Weapon specialWeapon = EnumWeapon.getRandomWeapon();

    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).startAttack();
        attackWeapon = EnumWeapon.getRandomWeapon();
        baseSDelay = attackWeapon.getSDelay();
        baseSpan = attackWeapon.getSpan();
        baseEDelay = attackWeapon.getEDelay();
        baseCooldown = attackWeapon.getBaseCooldown();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        GetSpecial(angle, self).startAttack();
        specialWeapon = EnumWeapon.getRandomWeapon();
    }

    private Attack InitBullet(float angle, Entity self)
    {
        return attackWeapon.GetAttack(angle, self);
    }

    private Attack InitLaser(float angle, Entity self)
    {
        return specialWeapon.GetSpecial(angle, self);
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject gun = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        gun.AddComponent<GunAttack>();
        gun.GetComponent<GunAttack>().transform.position = startPos;

        gun.GetComponent<GunAttack>().src = self;
        gun.GetComponent<GunAttack>().timeSpan = baseSpan;
        gun.GetComponent<GunAttack>().lenght = 1;
        gun.GetComponent<GunAttack>().followEntity = false;

        gun.GetComponent<GunAttack>().angle = angle;
        gun.GetComponent<GunAttack>().attack = attackWeapon;

        return gun.GetComponent<GunAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject bullet = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        bullet.AddComponent<GunAttack>();
        bullet.GetComponent<GunAttack>().transform.position = startPos;

        bullet.GetComponent<GunAttack>().src = self;
        bullet.GetComponent<GunAttack>().startDelay = 1.0f + specialWeapon.getSDelay();
        bullet.GetComponent<GunAttack>().timeSpan = specialWeapon.getSpan();
        bullet.GetComponent<GunAttack>().endDelay = specialWeapon.getEDelay();
        bullet.GetComponent<GunAttack>().lenght = 1;
        bullet.GetComponent<GunAttack>().followEntity = false;

        bullet.GetComponent<GunAttack>().angle = angle;
        bullet.GetComponent<GunAttack>().attack = specialWeapon;
        bullet.GetComponent<GunAttack>().isSpecial = true;

        return bullet.GetComponent<GunAttack>();
    }
}
