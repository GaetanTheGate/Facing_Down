using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MeleeWeapon
{
    public Shield() : this("Enemy") { }
    public Shield(string target) : base(target, "Shield")
    {
        baseAtk = 100f;
        baseRange = 1f;
        baseLenght = 1f;
        baseSpan = 0f;
        baseEDelay = 1f;
        baseCooldown = 0.0f;

        attackPath = "Prefabs/Items/Weapons/Shield";
        specialPath = "Prefabs/Items/Weapons/Shield";
        attackAudio = Resources.Load<AudioClip>("Sound_Effects/sword_swing");
        specialAudio = Resources.Load<AudioClip>("Sound_Effects/sword_swing");
    }

    //private SlashAttack.Way way = SlashAttack.Way.CounterClockwise;

    public override void WeaponAttack(float angle, Entity self)
    {

        GetAttack(angle, self).startAttack();

        /*if (way == SlashAttack.Way.Clockwise)
            way = SlashAttack.Way.CounterClockwise;
        else if (way == SlashAttack.Way.CounterClockwise)
            way = SlashAttack.Way.Clockwise;*/
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        WeaponAttack(angle, self);
    }

    public override Attack GetAttack(float angle, Entity self)
    {

        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = GetBaseDmg(self);
        AddHitAttack(swing, new DamageInfo(self, dmg, new Velocity(GetKnockbackIntensity(self, 2f), angle), baseSDelay + baseSpan + baseEDelay));

        swing.transform.position = startPos;
        swing.AddComponent<DirectAttack>();

        swing.GetComponent<DirectAttack>().audioClip = attackAudio;

        swing.GetComponent<DirectAttack>().src = self;
        swing.GetComponent<DirectAttack>().acceleration = 3.0f;
        swing.GetComponent<DirectAttack>().angle = angle;
        swing.GetComponent<DirectAttack>().range = baseRange;
        swing.GetComponent<DirectAttack>().lenght = baseLenght;
        swing.GetComponent<DirectAttack>().startDelay = baseSDelay;
        swing.GetComponent<DirectAttack>().timeSpan = baseSpan;
        swing.GetComponent<DirectAttack>().endDelay = baseEDelay;
        swing.GetComponent<DirectAttack>().followEntity = false;

        //swing.GetComponent<DirectAttack>().way = way;

        return swing.GetComponent<DirectAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        return GetAttack(angle, self);
    }

    //PASSIVE EFFECTS
    /*public override void OnPickup() {
        Game.player.stat.ModifyAcceleration(0.1f * Game.player.stat.BASE_ACCELERATION);
        Game.player.stat.ModifySpecialCooldown(-0.1f * Game.player.stat.GetSpecialCooldown());
    }

	public override DamageInfo OnDealDamage(DamageInfo damage) {
        damage.amount *= 1 + new Velocity(Game.player.self.GetComponent<Rigidbody2D>().velocity).getSpeed() / Game.player.stat.maxSpeed;
        return damage;
	}*/
}
