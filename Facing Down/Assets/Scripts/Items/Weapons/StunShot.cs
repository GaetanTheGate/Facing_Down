using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunShot : ProjectileWeapon
{
    //private float angleRange = 20.0f;

    public StunShot() : this("Enemy") { }
    public StunShot(string target) : base(target, "StunShot")
    {

        baseAtk = 30f;
        baseSpeed = 10f;
        baseSpan = 3f;
        baseEDelay = 7.0f;
        baseCooldown = -9;

        attackPath = "Prefabs/Weapons/StunShot";
        specialPath = "Prefabs/Weapons/StunShot";
        attackAudio = Resources.Load<AudioClip>("Sound_Effects/energy_ball");
        specialAudio = Resources.Load<AudioClip>("Sound_Effects/energy_ball");
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject stunShot = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo damageInfo = new DamageInfo(self, baseAtk * dmg, new Velocity(0.125f * dmg, angle), baseSDelay + baseSpan + baseEDelay);
        damageInfo.effects.Add(new StunEffect());
        AddHitAttack(stunShot, damageInfo);

        stunShot.AddComponent<ProjectileAttack>();
        stunShot.transform.position = startPos;

        stunShot.GetComponent<ProjectileAttack>().audioClip = attackAudio;

        stunShot.GetComponent<ProjectileAttack>().src = self;
        stunShot.GetComponent<ProjectileAttack>().layersToDestroyOn.Add(target);

        stunShot.GetComponent<ProjectileAttack>().angle = angle;
        stunShot.GetComponent<ProjectileAttack>().acceleration = 0f;
        stunShot.GetComponent<ProjectileAttack>().startDelay = baseSDelay;
        stunShot.GetComponent<ProjectileAttack>().timeSpan = baseSpan;
        stunShot.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        stunShot.GetComponent<ProjectileAttack>().speed = baseSpeed;
        //stunShot.GetComponent<ProjectileAttack>().isUsingEndAnimation = true;

        return stunShot.GetComponent<ProjectileAttack>();
    }

    //private int numberOfShot = 20;

    public float rangeMax = 20f;

    public override Attack GetSpecial(float angle, Entity self)
    {
        Transform following = null;

        /*for (float range = 1f; range < 20; range += 1){

        }*/
        Collider2D collider = Physics2D.OverlapCircle(self.transform.position, rangeMax, LayerMask.GetMask(target));
        if (collider != null)
            following = collider.transform;
        Debug.Log(following.gameObject.name);
        GameObject stunShot = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo damageInfo = new DamageInfo(self, baseAtk * dmg, new Velocity(0.125f * dmg, angle), baseSDelay + baseSpan + baseEDelay);
        damageInfo.effects.Add(new StunEffect());
        AddHitAttack(stunShot, damageInfo);

        stunShot.AddComponent<ProjectileAttack>();
        stunShot.transform.position = startPos;

        stunShot.GetComponent<ProjectileAttack>().audioClip = specialAudio;

        stunShot.GetComponent<ProjectileAttack>().src = self;
        stunShot.GetComponent<ProjectileAttack>().layersToDestroyOn.Add(target);

        stunShot.GetComponent<ProjectileAttack>().angle = angle;
        stunShot.GetComponent<ProjectileAttack>().acceleration = 0f;
        stunShot.GetComponent<ProjectileAttack>().startDelay = baseSDelay;
        stunShot.GetComponent<ProjectileAttack>().timeSpan = baseSpan;
        stunShot.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        stunShot.GetComponent<ProjectileAttack>().speed = baseSpeed;
        stunShot.GetComponent<ProjectileAttack>().followTransform = following;
        stunShot.GetComponent<ProjectileAttack>().followMaxAngle = 180f;
        //stunShot.GetComponent<ProjectileAttack>().isUsingEndAnimation = true;

        return stunShot.GetComponent<ProjectileAttack>();
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
