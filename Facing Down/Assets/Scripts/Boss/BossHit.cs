using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : MonoBehaviour
{
    public ParticleSystem particleEffect;

    public void getHit(DamageInfo dmgInfo)
    {
        /*Quaternion rotation = Quaternion.Euler(0, 0, dmgInfo.knockback.getAngle());
        ParticleSystem particle = Instantiate(particleEffect, transform.position, rotation);
        ParticleSystem.MainModule mainModule = particle.main;
        ParticleSystem.LimitVelocityOverLifetimeModule drag = particle.limitVelocityOverLifetime;

        drag.drag = new ParticleSystem.MinMaxCurve(5 / dmgInfo.knockback.getSpeed());*/

        Instantiate(particleEffect, transform.position, Quaternion.identity);
    }
}
