using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WipeOut : MeleeWeapon
{
    public WipeOut() : this("Enemy") { }
    public WipeOut(string target) : base(target, "WipeOut")
    {
        baseAtk = 500.0f;
        baseSDelay = 0.0f;
        baseSpan = 0.3f;
        baseEDelay = 0.2f;
        baseCooldown = 0.0f;
        baseRange = 100.0f;
        baseLenght = 0.0f;

        attackPath = "Prefabs/Weapons/Laser";
        specialPath = "Prefabs/Weapons/Laser";
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject explosion = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo dmgInfo = new DamageInfo(self, baseAtk * dmg, new Velocity(1f, angle));
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
        DamageInfo dmgInfo = new DamageInfo(self, baseAtk * dmg * 2, new Velocity(1f, angle));
        AddHitAttack(explosion, dmgInfo);

        explosion.transform.position = startPos;
        explosion.AddComponent<ExplosionAttack>();

        explosion.GetComponent<ExplosionAttack>().src = self;
        explosion.GetComponent<ExplosionAttack>().angle = angle;
        explosion.GetComponent<ExplosionAttack>().startDelay = baseSDelay + 0.5f;
        explosion.GetComponent<ExplosionAttack>().timeSpan = baseSpan * 2.0f;
        explosion.GetComponent<ExplosionAttack>().endDelay = baseEDelay * 2.0f;
        explosion.GetComponent<ExplosionAttack>().range = baseRange * 2.0f;
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
