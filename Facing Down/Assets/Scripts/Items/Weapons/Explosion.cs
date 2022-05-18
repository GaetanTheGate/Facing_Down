using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MeleeWeapon
{

    public Explosion() : this("Enemy") { }
    public Explosion(string target) : base(target, "Explosion")
    {
        baseAtk = 500.0f;
        baseSDelay = 0.0f;
        baseSpan = 0.2f;
        baseEDelay = 0.1f;
        baseCooldown = 0.0f;
        baseRange = 5.0f;
        baseLenght = 0.0f;

        attackPath = "Prefabs/Weapons/Laser";
        specialPath = "Prefabs/Weapons/Laser";
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject explosion = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo dmgInfo = new DamageInfo(self, baseAtk * dmg, new Velocity(1f, angle), baseSDelay + baseSpan + baseEDelay);
        AddHitAttack(explosion, dmgInfo);

        explosion.transform.position = startPos;
        explosion.AddComponent<ExplosionAttack>();

        explosion.GetComponent<ExplosionAttack>().src = self;
        explosion.GetComponent<ExplosionAttack>().angle = angle;
        explosion.GetComponent<ExplosionAttack>().startDelay = baseSDelay;
        explosion.GetComponent<ExplosionAttack>().timeSpan = baseSpan;
        explosion.GetComponent<ExplosionAttack>().endDelay = baseEDelay;
        explosion.GetComponent<ExplosionAttack>().range = baseRange;
        explosion.GetComponent<ExplosionAttack>().lenght = baseLenght;
        explosion.GetComponent<ExplosionAttack>().followEntity = false;

        return explosion.GetComponent<ExplosionAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject explosion = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo dmgInfo = new DamageInfo(self, dmg * baseAtk * 0.1f, new Velocity(1f, angle), 0.1f);
        AddHitAttack(explosion, dmgInfo);

        explosion.transform.position = startPos;
        explosion.AddComponent<ExplosionAttack>();

        explosion.GetComponent<ExplosionAttack>().src = self;
        explosion.GetComponent<ExplosionAttack>().angle = angle;
        explosion.GetComponent<ExplosionAttack>().startDelay = baseSDelay;
        explosion.GetComponent<ExplosionAttack>().timeSpan = baseSpan;
        explosion.GetComponent<ExplosionAttack>().endDelay = baseEDelay;
        explosion.GetComponent<ExplosionAttack>().range = baseRange;
        explosion.GetComponent<ExplosionAttack>().lenght = baseLenght;
        explosion.GetComponent<ExplosionAttack>().followEntity = false;

        return explosion.GetComponent<ExplosionAttack>();
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
