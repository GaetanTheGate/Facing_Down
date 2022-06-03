using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone3Hit : EnemyHit
{
    public ParticleSystem particleEffect1;
    public ParticleSystem particleEffect2;

    public override void getHit(DamageInfo dmgInfo)
    {
        base.getHit(dmgInfo);
        Quaternion rotation = Quaternion.Euler(0, 0, dmgInfo.knockback.getAngle());
        ParticleSystem particle = Instantiate(particleEffect1, transform.position, rotation);
        ParticleSystem.MainModule mainModule = particle.main;
        ParticleSystem.LimitVelocityOverLifetimeModule drag = particle.limitVelocityOverLifetime;

        drag.drag = new ParticleSystem.MinMaxCurve(Mathf.Min((5 / dmgInfo.knockback.getSpeed()) * 0.75f, 5 * 0.75f));

        StartCoroutine(launchSecondParticleEffect(0.15f, rotation, dmgInfo));
    }

    private IEnumerator launchSecondParticleEffect(float delay, Quaternion rotation, DamageInfo dmgInfo)
    {
        yield return new WaitForSeconds(delay);
        ParticleSystem particle2 = Instantiate(particleEffect2, transform.position, rotation);
        ParticleSystem.MainModule mainModule2 = particle2.main;
        mainModule2.maxParticles = 10;
        ParticleSystem.LimitVelocityOverLifetimeModule drag2 = particle2.limitVelocityOverLifetime;

        drag2.drag = new ParticleSystem.MinMaxCurve(Mathf.Min((5 / dmgInfo.knockback.getSpeed()) * 1.25f, 5 * 1.25f));
    }
}
