using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public Gun()
    {
        baseAtk = 10;
        baseRange = 30;
        baseSpan = 0.1f;
        baseCooldown = 0.0f;

        isAuto = true;

        attackPath = "Prefabs/Weapons/Gun";
        specialPath = "Prefabs/Weapons/Gun";
    }

    private Weapon attackWeapon = new Katana();
    private Weapon specialWeapon = new Katana();

    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).startAttack();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        GetSpecial(angle, self).startAttack();
    }

    private Attack InitBullet(float angle, Entity self)
    {/*
        GameObject bullet = GameObject.Instantiate(Resources.Load(bulletPath, typeof(GameObject)) as GameObject);
        bullet.AddComponent<ProjectileAttack>();

        bullet.GetComponent<ProjectileAttack>().src = self;
        bullet.GetComponent<ProjectileAttack>().tagsToDestroyOn.Add("Enemy");
        bullet.GetComponent<ProjectileAttack>().angle = angle;
        bullet.GetComponent<ProjectileAttack>().acceleration = 1.0f;
        bullet.GetComponent<ProjectileAttack>().endDelay = 5;
        bullet.GetComponent<ProjectileAttack>().speed = baseRange;
        */
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
        gun.GetComponent<GunAttack>().followEntity = forceUnFollow;

        float randomAngle = angle;
        randomAngle += Random.Range(-5.0f, 5.0f);

        gun.GetComponent<GunAttack>().angle = randomAngle;
        gun.GetComponent<GunAttack>().attack = attackWeapon;

        return gun.GetComponent<GunAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject bullet = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        bullet.AddComponent<GunAttack>();
        bullet.GetComponent<GunAttack>().transform.position = startPos;

        bullet.GetComponent<GunAttack>().src = self;
        bullet.GetComponent<GunAttack>().startDelay = 1f;
        bullet.GetComponent<GunAttack>().timeSpan = 1f;
        bullet.GetComponent<GunAttack>().endDelay = 1f;
        bullet.GetComponent<GunAttack>().lenght = 1;
        bullet.GetComponent<GunAttack>().followEntity = false;

        bullet.GetComponent<GunAttack>().angle = angle;
        bullet.GetComponent<GunAttack>().attack = specialWeapon;

        return bullet.GetComponent<GunAttack>();
    }
}
