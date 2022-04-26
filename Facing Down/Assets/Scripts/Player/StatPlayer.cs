using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatPlayer : StatEntity
{
    private PlayerIframes playerIframes;

    public Text hpText;

    private float rawAcceleration;
    private float acceleration = 10;
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
        rawAcceleration = acceleration;
    }

    public void ModifyAcceleration(float amount) {
        rawAcceleration += amount;
        acceleration = Mathf.Max(0, Mathf.Min(rawAcceleration, maxSpeed));
	}

    public float getAcceleration() {
        return acceleration;
	}

    [System.Obsolete("Damage should be passed as a DamageInfo instead of a float.")]
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
    
    public override void TakeDamage(DamageInfo damage)
    {
        if (!playerIframes.isIframe)
        {
            damage = Game.player.inventory.OnTakeDamage(damage);
            base.TakeDamage(damage);
            hpText.text = currentHitPoints.ToString();
            playerIframes.getIframe(2f);
        }
    }

	public override void checkIfDead() {
        if (currentHitPoints <= 0) Game.player.inventory.OnDeath();
		base.checkIfDead();
	}
}
