using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCyborgHit : EnemyHit
{
    public ParticleSystem particleEffect;

    public override void getHit(DamageInfo dmgInfo)
    {
        base.getHit(dmgInfo);
        Quaternion rotation = Quaternion.Euler(0, 0, dmgInfo.knockback.getAngle());
        ParticleSystem particle = Instantiate(particleEffect, transform.position, rotation);
        ParticleSystem.MainModule mainModule = particle.main;
        ParticleSystem.LimitVelocityOverLifetimeModule drag = particle.limitVelocityOverLifetime;

        //mainModule.startSpeed = new ParticleSystem.MinMaxCurve(1000, 2000);
        drag.drag = new ParticleSystem.MinMaxCurve(5 / dmgInfo.knockback.getSpeed());
    }
}
