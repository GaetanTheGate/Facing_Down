using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatPlayer : StatEntity
{
    private PlayerIframes playerIframes;

    public Text hpText;

    [Min(0)] public int numberOfDashes = 0;
    [Min(0)] public int maxDashes = 10;

    public override void Start()
    {
        base.Start();
        playerIframes = GetComponentInChildren<PlayerIframes>();
        hpText.text = currentHitPoints.ToString();
    }

    public void takeDamage(float damage, float iframeDuration = 2.0f)
    {
        if (!playerIframes.isIframe)
        {
            damage = Game.player.inventory.OnTakeDamage(damage);
            base.takeDamage(damage);
            hpText.text = currentHitPoints.ToString();
            playerIframes.getIframe(iframeDuration);
        }
    }

	public override void checkifDead() {
        if (currentHitPoints <= 0) Game.player.inventory.OnDeath();
		base.checkifDead();
	}
}
