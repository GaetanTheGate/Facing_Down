using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : Effect
{
    private float stunDuration;
    private string effectPath;

    public StunEffect(float stunDuration)
    {
        this.stunDuration = stunDuration;
        this.effectPath = "Prefabs/Particle_effects/StunStars";
    }

    public StunEffect() : this(2f) { }

    public override void OnHit(DamageInfo damageInfo)
    {
        Game.coroutineStarter.LaunchCoroutine(waitForStun(stunDuration, damageInfo.source.GetComponent<StatEntity>(), damageInfo.target.GetComponent<StatEntity>()));
    }

    public void setStunDuration(float stunDuration)
    {
        this.stunDuration = stunDuration;
    }

    public float getStunDuration()
    {
        return this.stunDuration;
    }

    private IEnumerator waitForStun(float duration, StatEntity source, StatEntity target)
    {
        StatPlayer statPlayer = null;
        if (target is StatPlayer) statPlayer = (StatPlayer)target;
        if (statPlayer != null) statPlayer.canIframe = false;
        target.Stun(true);

        GameObject stunStarsEffect = GameObject.Instantiate(Resources.Load<GameObject>(effectPath), target.transform);
        //stunStarsEffect.transform.parent = target.transform;
        stunStarsEffect.transform.localPosition = new Vector3(0, 0.2f, 0);
        ParticleSystem.MainModule main = stunStarsEffect.GetComponent<ParticleSystem>().main;
        main.startLifetime = duration;
        yield return new WaitForSeconds(duration);
        target.Stun(false);
        if (statPlayer != null) statPlayer.canIframe = true;
    }
}
