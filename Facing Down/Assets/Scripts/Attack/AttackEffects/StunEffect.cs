using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEffect : Effect
{
    private float stunDuration;

    public StunEffect(float stunDuration)
    {
        this.stunDuration = stunDuration;
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
        yield return new WaitForSeconds(duration);
        target.Stun(false);
        if (statPlayer != null) statPlayer.canIframe = true;
    }
}
