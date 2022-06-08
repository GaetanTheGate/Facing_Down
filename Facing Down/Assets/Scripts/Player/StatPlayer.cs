using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StatPlayer : StatEntity
{
    public readonly int BASE_HP = 1000;
    public readonly int BASE_ATK = 100;
    public readonly int BASE_CRIT_RATE = 5;
    public readonly int BASE_CRIT_DMG = 150;
    public readonly float BASE_ACCELERATION = 10;
    public readonly int BASE_MAX_DASH = 0;
    public readonly int BASE_MAX_SPECIAL = 0;
    public readonly float BASE_SPE_DURATION = 2;
    public readonly float BASE_SPE_COOLDOWN = 10;
    public readonly float BASE_CRITRATE = 5;
    public readonly float BASE_CRITDAMAGE = 150;

    private PlayerIframes playerIframes;

    private PlayerAttack playerAttack;
    private PlayerDash playerDash;
    private PlayerBulletTime playerBulletTime;

    public bool canIframe = true;

    //public Text hpText;

    private float acceleration;
    [Min(0.0f)] public float minAcceleration = 1;
    [Min(0.0f)] public float maxAcceleration = 20;
    [Min(0.0f)] public float maxSpeed = 50;

    private int numberOfDashes;
    private int maxDashes;

    private float specialCooldown;
    private float specialDuration;
    private int maxSpecial;
    private float specialLeft;

    private AudioClip hurtAudio;

    public override void Start()
    {
        InitStats(BASE_HP, BASE_ATK, BASE_CRIT_RATE, BASE_CRIT_DMG);
        base.Start();

        acceleration = BASE_ACCELERATION;
        numberOfDashes = 0;
        maxDashes = BASE_MAX_DASH;
        specialCooldown = BASE_SPE_COOLDOWN;
        specialDuration = BASE_SPE_DURATION;
        maxSpecial = BASE_MAX_SPECIAL;
        specialLeft = maxSpecial;
        critDmg = BASE_CRIT_DMG;
        critRate = BASE_CRIT_RATE;

        playerIframes = GetComponentInChildren<PlayerIframes>();

        playerAttack = transform.parent.gameObject.GetComponent<PlayerAttack>();
        playerDash = transform.parent.gameObject.GetComponent<PlayerDash>();
        playerBulletTime = transform.parent.gameObject.GetComponent<PlayerBulletTime>();

        UI.Init();
        UI.healthBar.UpdateHP();
        UI.specialBar.UpdateSpecial();
        UI.dashBar.UpdateDashes();

        hurtAudio = Resources.Load<AudioClip>("Sound_Effects/hurt");
    }

    public override void TakeDamage(DamageInfo damage)
    {
        if (isDead || (int)damage.amount == 0) return;
        if (!playerIframes.isIframe)
        {
            damage = Game.player.inventory.OnTakeDamage(damage);
            base.TakeDamage(damage);

            GameObject playerAudio = new GameObject("Player Audio");
            playerAudio.transform.parent = transform;
            playerAudio.AddComponent<AudioSource>();
            playerAudio.GetComponent<AudioSource>().PlayOneShot(hurtAudio);
            Destroy(playerAudio, 1f);

            Game.player.gameCamera.GetComponent<CameraManager>().Shake(0.1f, 0.3f);
            //hpText.text = currentHitPoints.ToString();
            if (canIframe) playerIframes.getIframe(Mathf.Min(2f, damage.hitCooldown));
        }

        UI.healthBar.UpdateHP();
    }

    public override void checkIfDead(DamageInfo lastDamageTaken) {
        if (currentHitPoints <= 0)
        {
            Game.player.inventory.OnDeath();
            if (currentHitPoints <= 0) {
                print("mort");
                if (onDeath != null) onDeath.Invoke();
                isDead = true;
                TextEndScene.text = Localization.GetUIString("textEndSceneDead").TEXT;
                EndSceneReset.destroy();
                SceneManager.LoadScene("EndScene");
            }
        }
    }

    public void ModifyAcceleration(float amount) {
        acceleration += amount;
    }

    public float GetAcceleration() {
        return Mathf.Max(minAcceleration, Mathf.Min(maxAcceleration, acceleration * Game.player.inventory.GetWeapon().stat.accelerationMult));
    }

	public override void ModifyMaxHP(int amount) {
		base.ModifyMaxHP(amount);
        UI.healthBar.UpdateHP();
	}

	public override void SetCurrentHP(int HP) {
		base.SetCurrentHP(HP);
        UI.healthBar.UpdateHP();
	}

    public override int GetMaxHP() {
        Game.player.Init();
        return Mathf.FloorToInt(maxHitPoints * Game.player.inventory.GetWeapon().stat.HPMult);
    }

	public int GetMaxDashes() {
        return maxDashes + Game.player.inventory.GetWeapon().stat.maxDashes;
	}

    public int GetRemainingDashes() {
        return GetMaxDashes() - numberOfDashes;
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

    public void ResetSpecial() {
        specialLeft = GetMaxSpecial();
        UI.specialBar.UpdateSpecial();
	}

    public int GetMaxSpecial() {
        return maxSpecial + Game.player.inventory.GetWeapon().stat.maxSpecial;
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
        specialLeft = Mathf.Min(GetMaxSpecial(), Mathf.Max(0, specialLeft + amount));
        UI.specialBar.UpdateSpecial();
	}

    public void ModifySpecialDuration(float amount) {
        specialDuration += amount;
	}

    public float GetSpecialDuration() {
        return specialDuration * Game.player.inventory.GetWeapon().stat.specialDurationMult;
	}

    public void ModifySpecialCooldown(float amount) {
        specialCooldown += amount;
	}

    public float GetSpecialCooldown() {
        return specialCooldown * Game.player.inventory.GetWeapon().stat.specialCooldownMult;
	}

    public override void Stun(bool shouldStun)
    {
        if (shouldStun)
        {
            if (playerAttack != null) playerAttack.canAttack = false;
            if (playerDash != null) playerDash.canDash = false;
            if (playerBulletTime != null) 
            {
                playerBulletTime.isInBulletTime = false;
                Game.time.SetGameSpeedInstant(1);
                playerBulletTime.canBulletTime = false;
            }
        }
        else
        {
            if (playerAttack != null) playerAttack.canAttack = true;
            if (playerDash != null) playerDash.canDash = true;
            if (playerBulletTime != null) playerBulletTime.canBulletTime = true;
        }
    }
}
