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
    [Min(0.0f)] public float maxAcceleration = 20;
    [Min(0.0f)] public float maxSpeed = 50;

    [Min(0)] public int numberOfDashes = 0;
    [Min(0)] public int maxDashes = 10;

    [Min(0)] public float specialCooldown = 10;
    [Min(0)] public float specialDuration = 2;
    [Min(0)] public int maxSpecial = 3;
    [Min(0)] public float specialLeft = 3;

    public override void Awake()
    {
        base.Awake();
        playerIframes = GetComponentInChildren<PlayerIframes>();
        hpText.text = currentHitPoints.ToString();
        rawAcceleration = acceleration;
    }

    public void ModifyAcceleration(float amount) {
        rawAcceleration += amount;
        acceleration = Mathf.Max(0, Mathf.Min(rawAcceleration, maxAcceleration));
	}

    public float getAcceleration() {
        return acceleration;
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

    /// <summary>
    /// Gives back special charges to the player.
    /// </summary>
    /// <param name="amount">The amount of special charge to give.</param>
    public void ReloadSpecial(float amount) {
        specialLeft = Mathf.Max(maxSpecial, specialLeft + amount);
	}
}
