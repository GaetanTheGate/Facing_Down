using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ProjectileWeapon
{
    private float angleRange = 20.0f;
    public Bullet(string target) : base(target)
    {

        baseAtk = 30f;
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

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(bullet, new DamageInfo(self, baseAtk * dmg, new Velocity(0.125f * dmg, angle)));

        bullet.AddComponent<ProjectileAttack>();
        bullet.transform.position = startPos;

        bullet.GetComponent<ProjectileAttack>().src = self;
        bullet.GetComponent<ProjectileAttack>().layersToDestroyOn.Add(target);

        bullet.GetComponent<ProjectileAttack>().angle = angle;
        bullet.GetComponent<ProjectileAttack>().acceleration = 1.0f;
        bullet.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        bullet.GetComponent<ProjectileAttack>().speed = baseSpeed;

        return bullet.GetComponent<ProjectileAttack>();
    }

    private int numberOfShot = 20;

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject bullet = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo dmgInfo = new DamageInfo(self, baseAtk * dmg, new Velocity(0.125f * dmg, angle));
        AddHitAttack(bullet, dmgInfo);

        bullet.AddComponent<ProjectileAttack>();
        bullet.transform.position = startPos;

        bullet.GetComponent<ProjectileAttack>().src = self;
        bullet.GetComponent<ProjectileAttack>().layersToDestroyOn.Add(target);

        bullet.GetComponent<ProjectileAttack>().acceleration = 1.0f;
        bullet.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        bullet.GetComponent<ProjectileAttack>().speed = baseSpeed;
        bullet.GetComponent<ProjectileAttack>().angle = Random.Range(angle - angleRange, angle + angleRange);

        GameObject attack = new GameObject();
        attack.AddComponent<CompositeAttack>();
        attack.GetComponent<CompositeAttack>().attackList.Add(bullet.GetComponent<ProjectileAttack>());

        for (int i = 1; i < numberOfShot; ++i)
        {
            GameObject newBullet = GameObject.Instantiate(bullet);
            newBullet.GetComponent<ProjectileAttack>().angle = Random.Range(angle - angleRange, angle + angleRange);
            newBullet.GetComponent<ProjectileAttack>().startDelay = i * (1.0f / numberOfShot);
            newBullet.GetComponent<AttackHit>().dmgInfo = dmgInfo;

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
