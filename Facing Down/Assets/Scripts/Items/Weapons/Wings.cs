using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : MeleeWeapon
{
    private float difference = 20.0f;

    public Wings() : this("Enemy") { }
    public Wings(string target) : base(target, "Wings")
    {

        baseAtk = 50;
        baseRange = 2.5f;
        baseLenght = 180;
        baseSpan = 0.3f;
        baseEDelay = 0.1f;
        baseCooldown = - baseEDelay - baseSpan / 2;

        stat.maxDashes = 5;
        stat.maxSpecial = 4;

        stat.accelerationMult = 0.75f;
        stat.HPMult = 0.8f;

        attackPath = "Prefabs/Items/Weapons/Wings";
        specialPath = "Prefabs/Items/Weapons/Wings";
        attackAudio = Resources.Load<AudioClip>("Sound_Effects/wing_flap_1");
        specialAudio = Resources.Load<AudioClip>("Sound_Effects/wing_flap_2");
    }


    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).startAttack();

        Game.coroutineStarter.LaunchCoroutine(onEndAttack(baseSpan, self, angle));
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo dmgInfo = new DamageInfo(self, baseAtk * dmg, new Velocity(2 * dmg, angle), baseSDelay + baseSpan + baseEDelay);
        AddHitAttack(swing, dmgInfo);

        swing.transform.position = startPos;
        swing.AddComponent<HalfSlashAttack>();

        swing.GetComponent<HalfSlashAttack>().audioClip = attackAudio;

        swing.GetComponent<HalfSlashAttack>().src = self;
        swing.GetComponent<HalfSlashAttack>().startDelay = baseSDelay;
        swing.GetComponent<HalfSlashAttack>().timeSpan = baseSpan;
        swing.GetComponent<HalfSlashAttack>().endDelay = baseEDelay;
        swing.GetComponent<HalfSlashAttack>().range = baseRange;
        swing.GetComponent<HalfSlashAttack>().lenght = baseLenght - difference;
        swing.GetComponent<HalfSlashAttack>().followEntity = forceUnFollow;
        swing.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;

        GameObject swing2 = GameObject.Instantiate(swing);
        swing2.GetComponent<AttackHit>().dmgInfo = dmgInfo;


        swing.GetComponent<HalfSlashAttack>().angle = angle - difference;
        swing.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.Clockwise;

        swing2.GetComponent<HalfSlashAttack>().angle = angle + difference;
        swing2.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.CounterClockwise;

        GameObject attack = new GameObject();
        attack.AddComponent<CompositeAttack>();
        attack.GetComponent<CompositeAttack>().attackList.Add(swing.GetComponent<HalfSlashAttack>());
        attack.GetComponent<CompositeAttack>().attackList.Add(swing2.GetComponent<HalfSlashAttack>());

        return attack.GetComponent<CompositeAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo dmgInfo = new DamageInfo(self, baseAtk * dmg * 5, new Velocity(4 * dmg, angle), baseSDelay * 2 + baseSpan * 2 + baseEDelay * 2);
        AddHitAttack(swing, dmgInfo);

        swing.transform.position = startPos;
        swing.AddComponent<HalfSlashAttack>();

        swing.GetComponent<HalfSlashAttack>().audioClip = specialAudio;

        swing.GetComponent<HalfSlashAttack>().src = self;
        swing.GetComponent<HalfSlashAttack>().startDelay = baseSDelay * 2;
        swing.GetComponent<HalfSlashAttack>().timeSpan = baseSpan * 2;
        swing.GetComponent<HalfSlashAttack>().endDelay = baseEDelay * 2;
        swing.GetComponent<HalfSlashAttack>().range = baseRange * 2;
        swing.GetComponent<HalfSlashAttack>().lenght = baseLenght - difference;
        swing.GetComponent<HalfSlashAttack>().followEntity = forceUnFollow;
        swing.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;

        GameObject swing2 = GameObject.Instantiate(swing);
        swing2.GetComponent<AttackHit>().dmgInfo = dmgInfo;


        swing.GetComponent<HalfSlashAttack>().angle = angle - difference;
        swing.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.Clockwise;

        swing2.GetComponent<HalfSlashAttack>().angle = angle + difference;
        swing2.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.CounterClockwise;


        GameObject attack = new GameObject();
        attack.AddComponent<CompositeAttack>();
        attack.GetComponent<CompositeAttack>().attackList.Add(swing.GetComponent<HalfSlashAttack>());
        attack.GetComponent<CompositeAttack>().attackList.Add(swing2.GetComponent<HalfSlashAttack>());

        return attack.GetComponent<CompositeAttack>();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        canAttack = false;
        canSpecial = false;

        GetSpecial(angle, self).startAttack();

        Game.coroutineStarter.LaunchCoroutine(onEndSpecial(baseSpan * 2, self, angle));
    }


    private IEnumerator onEndAttack(float delay, Entity self, float angle)
    {
        yield return new WaitForSeconds(delay);

        Velocity newVelo = new Velocity(baseAtk / 6, angle + 180);
        self.GetComponent<Rigidbody2D>().velocity += newVelo.GetAsVector2();
    }

    private IEnumerator onEndSpecial(float delay, Entity self, float angle)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log(angle);
        Velocity newVelo = new Velocity(baseAtk / 3, angle + 180);
        self.GetComponent<Rigidbody2D>().velocity += newVelo.GetAsVector2();

        canAttack = true;
        canSpecial = true;
    }

    public override void _Move(float angle, Entity self)
    {
        canMove = false;

        self.GetComponent<Rigidbody2D>().velocity = self.GetComponent<Rigidbody2D>().velocity * 0.2f;

        float gravitySpeed = self.GetComponent<GravityEntity>().gravity.getSpeed();
        self.GetComponent<GravityEntity>().gravity.setSpeed(0.00001f);

        Game.coroutineStarter.LaunchCoroutine(StartLooseGravity(0.5f, 10, self, gravitySpeed));
    }

    private IEnumerator StartLooseGravity(float delay, float duration, Entity self, float gravitySpeed)
    {
        yield return new WaitForSeconds(delay);

        Rigidbody2D rb = self.GetComponent<Rigidbody2D>();
        rb.velocity += new Velocity(self.GetComponent<GravityEntity>().gravity).SubToAngle(180).setSpeed(1).GetAsVector2();

        Game.coroutineStarter.LaunchCoroutine(RestoreGravity(duration, self, gravitySpeed));
    }

    private IEnumerator RestoreGravity(float duration, Entity self, float speed)
    {
        yield return new WaitForSeconds(duration);
        self.GetComponent<GravityEntity>().gravity.setSpeed(speed);

        canMove = true;
    }

	//PASSIVE EFFECTS
	public override void OnPickup() {
        Game.player.stat.ModifyMaxDashes(1);
        Game.player.stat.ModifySpecialCooldown(0.90f);
	}

    private float lastLeftGround = 0;
    private float maxAirTimeBuff = 30;
	public override void OnGroundCollisionLeave() {
        lastLeftGround = Time.time;
	}

	public override void OnGroundCollisionEnter() {
        lastLeftGround = 0;
	}

	public override DamageInfo OnDealDamage(DamageInfo damage) {
        if (lastLeftGround == 0) return damage;
        damage.amount *= 1 + (Time.time - lastLeftGround) / maxAirTimeBuff;
        return damage;
	}
}
