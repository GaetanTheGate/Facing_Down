using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarAxe : MeleeWeapon
{
    private bool isFirstSpin = true;
    private GameObject axeAudio;

    public WarAxe() : this("Enemy") { }
    public WarAxe(string target) : base(target, "WarAxe")
    {
        baseAtk = 150;
        baseRange = 3.5f;
        baseLenght = 225;
        baseSpan = 0.6f;
        baseSDelay = 0.3f;
        baseEDelay = 0.1f;
        baseCooldown = 0.1f;

        stat.maxDashes = 5;
        stat.maxSpecial = 4;

        stat.HPMult = 1.25f;

        attackPath = "Prefabs/Items/Weapons/WarAxe";
        specialPath = "Prefabs/Items/Weapons/WarAxe";
        attackAudio = Resources.Load<AudioClip>("Sound_Effects/axe_swing_1");
        specialAudio = Resources.Load<AudioClip>("Sound_Effects/axe_swing_2");
    }

    private SwingAttack.Way way = SwingAttack.Way.CounterClockwise;

    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).GetComponent<SwingAttack>().startAttack();


        if (way == SwingAttack.Way.Clockwise)
            way = SwingAttack.Way.CounterClockwise;
        else if (way == SwingAttack.Way.CounterClockwise)
            way = SwingAttack.Way.Clockwise;
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        canAttack = false;

        GetSpecial(angle, self).startAttack();
    }

    private void nextSpin(Entity self, float angle)
    {
        if (self.GetComponent<EntityCollisionStructure>().isGrounded)
        {
            GameObject.Destroy(axeAudio);
            isFirstSpin = true;
            canAttack = true;
            return;
        }

        self.transform.rotation = Quaternion.Euler(0, 0, angle);

        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = GetBaseDmg(self);
        AddHitAttack(swing, new DamageInfo(self, dmg * 0.5f, new Velocity(0.5f * dmg, angle), baseSDelay + baseSpan + baseEDelay));

        swing.transform.position = startPos;
        swing.AddComponent<SwingAttack>();

        if (isFirstSpin)
        {
            isFirstSpin = false;
            axeAudio = new GameObject("Axe Audio");
            axeAudio.transform.parent = self.transform;
            axeAudio.AddComponent<AudioSource>();

            axeAudio.GetComponent<AudioSource>().volume = 0.5f;
            axeAudio.GetComponent<AudioSource>().clip = specialAudio;
            axeAudio.GetComponent<AudioSource>().loop = true;
            axeAudio.GetComponent<AudioSource>().Play();
        }
        

        swing.GetComponent<SwingAttack>().src = self;
        swing.GetComponent<SwingAttack>().acceleration = 1f;
        swing.GetComponent<SwingAttack>().angle = angle - 10;
        swing.GetComponent<SwingAttack>().range = baseRange;
        swing.GetComponent<SwingAttack>().lenght = 10;
        swing.GetComponent<SwingAttack>().timeSpan = 0.01f;
        swing.GetComponent<SwingAttack>().followEntity = forceUnFollow;

        swing.GetComponent<SwingAttack>().way = SwingAttack.Way.Clockwise;

        swing.GetComponent<SwingAttack>().onEndAttack += nextSpin;
        swing.GetComponent<SwingAttack>().startAttack();
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = GetBaseDmg(self);
        AddHitAttack(swing, new DamageInfo(self, dmg, new Velocity(5 * dmg, angle), baseSDelay + baseSpan + baseEDelay));

        swing.transform.position = startPos;
        swing.AddComponent<SwingAttack>();

        swing.GetComponent<SwingAttack>().audioClip = attackAudio;

        swing.GetComponent<SwingAttack>().src = self;
        swing.GetComponent<SwingAttack>().acceleration = 0.7f;
        swing.GetComponent<SwingAttack>().angle = angle;
        swing.GetComponent<SwingAttack>().range = baseRange;
        swing.GetComponent<SwingAttack>().lenght = baseLenght;
        swing.GetComponent<SwingAttack>().timeSpan = baseSpan;
        swing.GetComponent<SwingAttack>().startDelay = baseSDelay;
        swing.GetComponent<SwingAttack>().endDelay = baseEDelay;
        swing.GetComponent<SwingAttack>().followEntity = forceUnFollow;

        swing.GetComponent<SwingAttack>().way = way;

        return swing.GetComponent<SwingAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = GetBaseDmg(self);
        AddHitAttack(swing, new DamageInfo(self, dmg * 2, new Velocity(5 * dmg, angle), baseSDelay + baseSpan + baseEDelay));

        swing.transform.position = startPos;
        swing.AddComponent<SwingAttack>();

        swing.GetComponent<SwingAttack>().audioClip = null;

        swing.GetComponent<SwingAttack>().src = self;
        swing.GetComponent<SwingAttack>().acceleration = 0.6f;
        swing.GetComponent<SwingAttack>().angle = 90;
        swing.GetComponent<SwingAttack>().range = baseRange;
        swing.GetComponent<SwingAttack>().lenght = 0;
        swing.GetComponent<SwingAttack>().timeSpan = 0.0f;
        swing.GetComponent<SwingAttack>().startDelay = 1.0f;
        swing.GetComponent<SwingAttack>().followEntity = forceUnFollow;

        swing.GetComponent<SwingAttack>().way = SwingAttack.Way.Clockwise;

        swing.GetComponent<SwingAttack>().onEndAttack += nextSpin;

        return swing.GetComponent<SwingAttack>();
    }

    //PASSIVE EFFECTS
    public override void OnPickup() {
        Game.player.stat.ModifyMaxHP(Mathf.FloorToInt(Game.player.stat.BASE_HP * 0.10f));
        Game.player.stat.SetCurrentHP(Game.player.stat.GetCurrentHP() + Mathf.FloorToInt(0.10f * Game.player.stat.BASE_HP * Game.player.inventory.GetWeapon().stat.HPMult));
        Game.player.stat.ModifyAtk(Game.player.stat.BASE_ATK * 0.10f);
    }

    private readonly float hpTheshold = 0.25f;
    private int activeBuffs = 0;
    private readonly float buffDuration = 5;
    private readonly float buffStrength = 2;

	public override DamageInfo OnTakeDamage(DamageInfo damage) {
        if (Game.player.stat.GetCurrentHP() < Game.player.stat.GetMaxHP() * hpTheshold) {
            ++activeBuffs;
            Game.coroutineStarter.StartCoroutine(startBuffDecayRoutine());
        }
        return base.OnTakeDamage(damage);
	}

	public override DamageInfo OnDealDamage(DamageInfo damage) {
        if (activeBuffs > 0) damage.amount *= buffStrength;
		return damage;
	}

    private IEnumerator startBuffDecayRoutine() {
        yield return new WaitForSeconds(buffDuration);
        --activeBuffs;
    }
}
