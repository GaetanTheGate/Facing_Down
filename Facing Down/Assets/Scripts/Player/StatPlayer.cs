using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StatPlayer : StatEntity
{
    private PlayerIframes playerIframes;

    //public Text hpText;

    private float rawAcceleration;
    private float acceleration = 10;
    [Min(0.0f)] public float maxAcceleration = 20;
    [Min(0.0f)] public float maxSpeed = 50;

    private int numberOfDashes = 0;
    [SerializeField] private int maxDashes = 10;

    [Min(0)] public float specialCooldown = 10;
    [Min(0)] public float specialDuration = 2;
    [SerializeField] private int maxSpecial = 3;
    private float specialLeft = 3;

    public override void Start()
    {
        InitStats(1000, 100, 5, 150);
        base.Start();
        playerIframes = GetComponentInChildren<PlayerIframes>();
        //hpText.text = currentHitPoints.ToString();
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
        UI.healthBar.UpdateHP();
        Debug.Log("TOOK DAMAGE");
        if (isDead) return;
        if (!playerIframes.isIframe)
        {
            damage = Game.player.inventory.OnTakeDamage(damage);
            base.TakeDamage(damage);
            //hpText.text = currentHitPoints.ToString();
            playerIframes.getIframe(2f);
        }
    }

    public override void checkIfDead(DamageInfo lastDamageTaken) {
        if (currentHitPoints <= 0)
        {
            Game.player.inventory.OnDeath();
            if (onDeath != null && currentHitPoints <= 0)
                onDeath.Invoke();
            isDead = true;
        }
    }

    /// <summary>
    /// Gives back special charges to the player.
    /// </summary>
    /// <param name="amount">The amount of special charge to give.</param>
    public void ReloadSpecial(float amount) {
        specialLeft = Mathf.Max(maxSpecial, specialLeft + amount);
	}

	public override void ModifyMaxHP(int amount) {
		base.ModifyMaxHP(amount);
        UI.healthBar.UpdateHP();
	}

	public override void SetCurrentHP(int HP) {
		base.SetCurrentHP(HP);
        UI.healthBar.UpdateHP();
	}

    public int GetRemainingDashes() {
        return maxDashes - numberOfDashes;
	}

    public void UseDashes(int amount) {
        numberOfDashes += amount;
        UI.dashBar.UpdateDashes();
	}

    public void ModifyMaxDashes(int amount) {
        maxDashes += amount;
        UI.dashBar.UpdateDashes();
	}

    public void ResetDashes() {
        numberOfDashes = 0;
        UI.dashBar.UpdateDashes();
	}

    public int GetMaxSpecial() {
        return maxSpecial;
	}

    public float GetSpecialLeft() {
        return specialLeft;
	}

    public void ModifyMaxSpecial(int amount) {
        maxSpecial += amount;
        specialLeft += amount;
        UI.specialBar.UpdateSpecial();
	}

    public void ModifySpecialLeft(float amount) {
        specialLeft += amount;
        UI.specialBar.UpdateSpecial();
	}
}
