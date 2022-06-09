using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MeleeWeapon
{
    public Laser() : this("Enemy", "Laser") { }
    public Laser(string target) : this(target, "Laser") { }
    
    public Laser(string target, string id) : base(target, id)
    {
        baseAtk = 100;
        baseRange = 50;
        baseLenght = 1;
        baseSpan = 0.2f;
        baseEDelay = 0.2f;
        baseCooldown = 0.1f;

        attackPath = "Prefabs/Items/Weapons/Laser";
        specialPath = "Prefabs/Items/Weapons/Laser";
        attackAudio = Resources.Load<AudioClip>("Sound_Effects/Laser Weapons Sound Pack/continuous_beam_3");
        specialAudio = Resources.Load<AudioClip>("Sound_Effects/Laser Weapons Sound Pack/continuous_beam_3");
    }

    public float hitPerSecond = 10;

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject laser = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo dmgInfo = new DamageInfo(self, baseAtk * dmg, new Velocity(0.125f * dmg, angle), baseSDelay + baseSpan + baseEDelay);
        AddHitAttack(laser, dmgInfo);

        laser.transform.position = startPos;
        laser.AddComponent<LaserAttack>();

        laser.GetComponent<LaserAttack>().audioClip = attackAudio;

        laser.GetComponent<LaserAttack>().src = self;
        laser.GetComponent<LaserAttack>().angle = angle;
        laser.GetComponent<LaserAttack>().range = baseRange;
        laser.GetComponent<LaserAttack>().lenght = baseLenght;
        laser.GetComponent<LaserAttack>().startDelay = baseSDelay;
        laser.GetComponent<LaserAttack>().timeSpan = baseSpan;
        laser.GetComponent<LaserAttack>().endDelay = baseEDelay;

        laser.GetComponent<LaserAttack>().followEntity = forceUnFollow;

        return laser.GetComponent<LaserAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        float lenghtAtk = 1.0f / hitPerSecond;

        GameObject laser = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo dmgInfo = new DamageInfo(self, baseAtk * dmg * lenghtAtk, new Velocity(0.125f * dmg, angle), lenghtAtk);
        AddHitAttack(laser, dmgInfo);

        laser.transform.position = startPos;
        laser.AddComponent<LaserAttack>();

        laser.GetComponent<LaserAttack>().audioClip = specialAudio;

        laser.GetComponent<LaserAttack>().src = self;
        laser.GetComponent<LaserAttack>().angle = angle;
        laser.GetComponent<LaserAttack>().range = baseRange;
        laser.GetComponent<LaserAttack>().lenght = baseLenght;
        laser.GetComponent<LaserAttack>().startDelay = baseSDelay;
        laser.GetComponent<LaserAttack>().timeSpan = baseSpan;
        laser.GetComponent<LaserAttack>().endDelay = baseEDelay;

        laser.GetComponent<LaserAttack>().followEntity = forceUnFollow;

        return laser.GetComponent<LaserAttack>();
    }

    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).startAttack();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        GetSpecial(angle, self).startAttack();
    }
}
