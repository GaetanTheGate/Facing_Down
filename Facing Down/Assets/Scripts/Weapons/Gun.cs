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

        attackPath = "Prefabs/Weapons/Bullet";
        specialPath = "Prefabs/Weapons/Bullet";
    }

    private string bulletPath = "Prefabs/Weapons/Bullet";

    public override void Attack(float angle, Entity self)
    {
        GameObject bullet = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        bullet.AddComponent<GunAttack>();
        bullet.GetComponent<GunAttack>().transform.position = self.transform.position;

        bullet.GetComponent<GunAttack>().src = self;
        bullet.GetComponent<GunAttack>().timeSpan = baseSpan;
        bullet.GetComponent<GunAttack>().lenght = 1;
        bullet.GetComponent<GunAttack>().followEntity = true;

        float randomAngle = angle;
        randomAngle += Random.Range(-5.0f, 5.0f);

        bullet.GetComponent<GunAttack>().angle = randomAngle;
        bullet.GetComponent<GunAttack>().attack = InitBullet(randomAngle, self);

        bullet.GetComponent<GunAttack>().startAttack();
    }

    public override void Special(float angle, Entity self)
    {
        GameObject bullet = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        bullet.AddComponent<GunAttack>();
        bullet.GetComponent<GunAttack>().transform.position = self.transform.position;

        bullet.GetComponent<GunAttack>().src = self;
        bullet.GetComponent<GunAttack>().startDelay = 1f;
        bullet.GetComponent<GunAttack>().timeSpan = 1f;
        bullet.GetComponent<GunAttack>().endDelay = 1f;
        bullet.GetComponent<GunAttack>().lenght = 1;
        bullet.GetComponent<GunAttack>().followEntity = false;

        float randomAngle = angle;
        randomAngle += Random.Range(-5.0f, 5.0f);

        bullet.GetComponent<GunAttack>().angle = randomAngle;
        bullet.GetComponent<GunAttack>().attack = InitBullet(randomAngle, self);

        bullet.GetComponent<GunAttack>().startAttack();
    }

    private ProjectileAttack InitBullet(float angle, Entity self)
    {
        GameObject bullet = GameObject.Instantiate(Resources.Load(bulletPath, typeof(GameObject)) as GameObject);
        bullet.AddComponent<ProjectileAttack>();

        bullet.GetComponent<ProjectileAttack>().src = self;
        bullet.GetComponent<ProjectileAttack>().tagsToDestroyOn.Add("Enemy");
        bullet.GetComponent<ProjectileAttack>().angle = angle;
        bullet.GetComponent<ProjectileAttack>().acceleration = 1.0f;
        bullet.GetComponent<ProjectileAttack>().endDelay = 5;
        bullet.GetComponent<ProjectileAttack>().speed = baseRange;

        return bullet.GetComponent<ProjectileAttack>();
    }
}
