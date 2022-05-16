using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MeleeWeapon
{
    public Gun(string target) : base(target, "Gun")
    {
        attackWeapon = new Bullet(target);
        specialWeapon = new Laser(target);

        baseSpan = 0.1f;
        baseCooldown = 0.0f;

        isAuto = true;

        attackPath = "Prefabs/Weapons/Gun";
        specialPath = "Prefabs/Weapons/Gun";
    }

    private Weapon attackWeapon;
    private Weapon specialWeapon;

    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).startAttack();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        GetSpecial(angle, self).startAttack();
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject gun = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        gun.AddComponent<GunAttack>();
        gun.GetComponent<GunAttack>().transform.position = startPos;

        gun.GetComponent<GunAttack>().src = self;
        gun.GetComponent<GunAttack>().timeSpan = baseSpan;
        gun.GetComponent<GunAttack>().lenght = 1;
        gun.GetComponent<GunAttack>().followEntity = forceUnFollow;

        float randomAngle = angle;
        randomAngle += Random.Range(-5.0f, 5.0f);

        gun.GetComponent<GunAttack>().angle = randomAngle;
        gun.GetComponent<GunAttack>().attack = attackWeapon;

        return gun.GetComponent<GunAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject gun = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);
        gun.AddComponent<GunAttack>();
        gun.GetComponent<GunAttack>().transform.position = startPos;

        gun.GetComponent<GunAttack>().src = self;
        gun.GetComponent<GunAttack>().startDelay = 1.0f;
        gun.GetComponent<GunAttack>().timeSpan = 1;
        gun.GetComponent<GunAttack>().endDelay = 0.5f;
        gun.GetComponent<GunAttack>().lenght = 1;
        gun.GetComponent<GunAttack>().followEntity = false;

        gun.GetComponent<GunAttack>().angle = angle;
        gun.GetComponent<GunAttack>().attack = specialWeapon;
        gun.GetComponent<GunAttack>().isSpecial = true;

        return gun.GetComponent<GunAttack>();
    }
}
