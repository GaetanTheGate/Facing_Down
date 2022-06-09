using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionBreaker : MeleeWeapon
{
    public DimensionBreaker() : this("Enemy") { }
    public DimensionBreaker(string target) : base(target, "DimensionBreaker")
    {
        attackWeapon = EnumWeapon.getRandomWeapon(target);
        specialWeapon = EnumWeapon.getRandomWeapon(target);

        baseSDelay = attackWeapon.getSDelay();
        baseSpan = attackWeapon.getSpan();
        baseEDelay = attackWeapon.getEDelay();
        baseCooldown = attackWeapon.getBaseCooldown();

        isAuto = true;

        attackPath = "Prefabs/Items/Weapons/Gun";
        specialPath = "Prefabs/Items/Weapons/Gun";
    }

    private Weapon attackWeapon;
    private Weapon specialWeapon;

    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).startAttack();
        attackWeapon = EnumWeapon.getRandomWeapon(target);
        baseSDelay = attackWeapon.getSDelay();
        baseSpan = attackWeapon.getSpan();
        baseEDelay = attackWeapon.getEDelay();
        baseCooldown = attackWeapon.getBaseCooldown();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        GetSpecial(angle, self).startAttack();
        specialWeapon = EnumWeapon.getRandomWeapon(target);
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
