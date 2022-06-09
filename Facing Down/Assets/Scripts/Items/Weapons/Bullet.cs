using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ProjectileWeapon
{
    private float angleRange = 20.0f;

    public Bullet() : this("Enemy") { }
    public Bullet(string target) : base(target, "Bullet")
    {

        baseAtk = 30f;
        baseSpeed = 20.0f;
        baseSpan = 0.0f;
        baseEDelay = 5.0f;
        baseCooldown = - baseEDelay + 0.1f;

        attackPath = "Prefabs/Items/Weapons/Bullet";
        specialPath = "Prefabs/Items/Weapons/Bullet";
        attackAudio = Resources.Load<AudioClip>("Sound_Effects/Laser Weapons Sound Pack/light_blast_5");
        specialAudio = Resources.Load<AudioClip>("Sound_Effects/Laser Weapons Sound Pack/light_blast_5");
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject bullet = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = GetBaseDmg(self);
        AddHitAttack(bullet, new DamageInfo(self, dmg, new Velocity(GetKnockbackIntensity(self, 0.125f), angle), baseSDelay + baseSpan + baseEDelay));

        bullet.AddComponent<ProjectileAttack>();
        bullet.transform.position = startPos;

        bullet.GetComponent<ProjectileAttack>().audioClip = attackAudio;

        bullet.GetComponent<ProjectileAttack>().src = self;
        bullet.GetComponent<ProjectileAttack>().layersToDestroyOn.Add(target);

        bullet.GetComponent<ProjectileAttack>().angle = angle;
        bullet.GetComponent<ProjectileAttack>().acceleration = 1.0f;
        bullet.GetComponent<ProjectileAttack>().startDelay = baseSDelay;
        bullet.GetComponent<ProjectileAttack>().timeSpan = baseSpan;
        bullet.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        bullet.GetComponent<ProjectileAttack>().speed = baseSpeed;

        return bullet.GetComponent<ProjectileAttack>();
    }

    private int numberOfShot = 20;

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject bullet = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = GetBaseDmg(self);
        DamageInfo dmgInfo = new DamageInfo(self, dmg, new Velocity(GetKnockbackIntensity(self, 0.125f), angle), baseSDelay + baseSpan + baseEDelay);
        AddHitAttack(bullet, dmgInfo);

        bullet.AddComponent<ProjectileAttack>();
        bullet.transform.position = startPos;

        bullet.GetComponent<ProjectileAttack>().src = self;
        bullet.GetComponent<ProjectileAttack>().layersToDestroyOn.Add(target);

        bullet.GetComponent<ProjectileAttack>().acceleration = 1.0f;
        bullet.GetComponent<ProjectileAttack>().startDelay = baseSDelay;
        bullet.GetComponent<ProjectileAttack>().timeSpan = baseSpan;
        bullet.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        bullet.GetComponent<ProjectileAttack>().speed = baseSpeed;
        bullet.GetComponent<ProjectileAttack>().angle = Random.Range(angle - angleRange, angle + angleRange);

        GameObject attack = new GameObject();
        attack.AddComponent<CompositeAttack>();
        attack.GetComponent<CompositeAttack>().attackList.Add(bullet.GetComponent<ProjectileAttack>());

        for (int i = 1; i < numberOfShot; ++i)
        {
            GameObject newBullet = GameObject.Instantiate(bullet);
            if (i % 3 == 1) newBullet.GetComponent<ProjectileAttack>().audioClip = specialAudio;
            newBullet.GetComponent<ProjectileAttack>().angle = Random.Range(angle - angleRange, angle + angleRange);
            newBullet.GetComponent<ProjectileAttack>().startDelay = baseSDelay + i * (1.0f / numberOfShot);
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
