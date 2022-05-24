using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCyborgDeath : EnemyDeath
{
    public ParticleSystem particleEffectExplosion;

    public override void die()
    {
        base.die();
        Instantiate(particleEffectExplosion, transform.position, Quaternion.identity);
        Destroy(parentToDestroy);
    }
}
