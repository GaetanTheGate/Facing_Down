using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ProjectileWeapon
{
    private float angleRange = 20.0f;
    public Bullet(string target) : base(target)
    {

        baseAtk = 1f;
        baseSpeed = 20.0f;
        baseSpan = 0.0f;
        baseEDelay = 5.0f;
        baseCooldown = - baseEDelay + 0.1f;

        attackPath = "Prefabs/Weapons/Bullet";
        specialPath = "Prefabs/Weapons/Bullet";
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject bullet = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        bullet.AddComponent<ProjectileAttack>();
        bullet.transform.position = startPos;

        bullet.GetComponent<ProjectileAttack>().src = self;
        bullet.GetComponent<ProjectileAttack>().tagsToDestroyOn.Add(target);

        bullet.GetComponent<ProjectileAttack>().angle = angle;
        bullet.GetComponent<ProjectileAttack>().acceleration = 1.0f;
        bullet.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        bullet.GetComponent<ProjectileAttack>().speed = baseSpeed;


        AddHitAttack(bullet, baseAtk);
        return bullet.GetComponent<ProjectileAttack>();
    }

    private int numberOfShot = 20;

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject bullet = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        bullet.AddComponent<ProjectileAttack>();
        bullet.transform.position = startPos;

        bullet.GetComponent<ProjectileAttack>().src = self;
        bullet.GetComponent<ProjectileAttack>().tagsToDestroyOn.Add(target);

        bullet.GetComponent<ProjectileAttack>().acceleration = 1.0f;
        bullet.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        bullet.GetComponent<ProjectileAttack>().speed = baseSpeed;
        bullet.GetComponent<ProjectileAttack>().angle = Random.Range(angle - angleRange, angle + angleRange);
        AddHitAttack(bullet, baseAtk);

        GameObject attack = new GameObject();
        attack.AddComponent<CompositeAttack>();
        attack.GetComponent<CompositeAttack>().attackList.Add(bullet.GetComponent<ProjectileAttack>());

        for (int i = 0; i < numberOfShot - 1; ++i)
        {
            GameObject newBullet = GameObject.Instantiate(bullet);
            newBullet.GetComponent<ProjectileAttack>().angle = Random.Range(angle - angleRange, angle + angleRange);
            newBullet.GetComponent<ProjectileAttack>().startDelay = (i + 1) * (1.0f / numberOfShot);

            attack.GetComponent<CompositeAttack>().attackList.Add(newBullet.GetComponent<ProjectileAttack>());
        }


        return attack.GetComponent<CompositeAttack>();
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
