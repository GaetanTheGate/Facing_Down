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

        attackPath = "Prefabs/Weapons/Shield";
        specialPath = "Prefabs/Weapons/Shield";
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

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(swing, new DamageInfo(self, baseAtk * dmg, new Velocity(2 * dmg, angle), baseSDelay + baseSpan + baseEDelay));

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
        GameObject laser = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(laser, new DamageInfo(self, baseAtk * dmg * 5, new Velocity(0.5f * dmg, angle), 1)) ;

        laser.transform.position = startPos;
        laser.AddComponent<LaserAttack>();

        laser.GetComponent<LaserAttack>().audioClip = specialAudio;

        laser.GetComponent<LaserAttack>().src = self;
        laser.GetComponent<LaserAttack>().angle = angle;
        laser.GetComponent<LaserAttack>().range = baseRange * 3;
        laser.GetComponent<LaserAttack>().lenght = self.transform.localScale.x;
        laser.GetComponent<LaserAttack>().startDelay = 0;
        laser.GetComponent<LaserAttack>().timeSpan = 0;
        laser.GetComponent<LaserAttack>().endDelay = 0.05f;
        laser.GetComponent<LaserAttack>().followEntity = false;

        return laser.GetComponent<LaserAttack>();
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
