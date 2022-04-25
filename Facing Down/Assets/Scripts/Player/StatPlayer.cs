using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatPlayer : StatEntity
{
    private PlayerIframes playerIframes;

    public Text hpText;

    [Min(0.0f)] public float acceleration = 1;
    [Min(0.0f)] public float maxSpeed = 50;

    [Min(0)] public int numberOfDashes = 0;
    [Min(0)] public int maxDashes = 10;

    [Min(0)] public float specialCooldown = 10;
    [Min(0)] public float specialDuration = 2;
    [Min(0)] public int maxSpecial = 3;
    [Min(0)] public float specialLeft = 3;

    public override void Start()
    {
        base.Start();
        playerIframes = GetComponentInChildren<PlayerIframes>();
        hpText.text = currentHitPoints.ToString();
    }

    public override void takeDamage(float damage)
    {
        if (!playerIframes.isIframe)
        {
            damage = Game.player.inventory.OnTakeDamage(damage);
            base.takeDamage(damage);
            hpText.text = currentHitPoints.ToString();
            playerIframes.getIframe(2f);
        }
    }

	public override void checkIfDead() {
        if (currentHitPoints <= 0) Game.player.inventory.OnDeath();
		base.checkIfDead();
	}
}
