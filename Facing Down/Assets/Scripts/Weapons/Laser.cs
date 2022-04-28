using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MeleeWeapon
{
    public Laser(string target) : base(target)
    {
        baseAtk = 20;
        baseRange = 3;
        baseLenght = 1;
        baseSpan = 0.3f;
        baseCooldown = 0.1f;

        attackPath = "Prefabs/Weapons/Laser";
        specialPath = "Prefabs/Weapons/Laser";
    }

    private int hitPerSecond = 10;

    public override Attack GetAttack(float angle, Entity self)
    {
        float lenghtAtk = 1.0f /  hitPerSecond;

        GameObject laser = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);
        laser.transform.position = startPos;
        laser.AddComponent<LaserAttack>();

        laser.GetComponent<LaserAttack>().src = self;
        laser.GetComponent<LaserAttack>().angle = angle;
        laser.GetComponent<LaserAttack>().range = 50;
        laser.GetComponent<LaserAttack>().lenght = baseLenght;
        laser.GetComponent<LaserAttack>().timeSpan = lenghtAtk;
        laser.GetComponent<LaserAttack>().endDelay = 0.0f;

        laser.GetComponent<LaserAttack>().followEntity = forceUnFollow;

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(laser, new DamageInfo(self, baseAtk * dmg, new Velocity(0.125f * dmg, angle)));

        GameObject attack = new GameObject();
        attack.AddComponent<CompositeAttack>();
        attack.GetComponent<CompositeAttack>().attackList.Add(laser.GetComponent<LaserAttack>());
        
        for (float startDelay = lenghtAtk; startDelay < baseSpan; startDelay += lenghtAtk){

            GameObject newLaser = GameObject.Instantiate(laser);
            newLaser.GetComponent<LaserAttack>().startDelay = startDelay;

            attack.GetComponent<CompositeAttack>().attackList.Add(newLaser.GetComponent<LaserAttack>());
        }

        return attack.GetComponent<CompositeAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        float lenghtAtk = 1.0f / hitPerSecond;

        GameObject laser = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);
        laser.transform.position = startPos;
        laser.AddComponent<LaserAttack>();

        laser.GetComponent<LaserAttack>().src = self;
        laser.GetComponent<LaserAttack>().angle = angle;
        laser.GetComponent<LaserAttack>().range = 50;
        laser.GetComponent<LaserAttack>().lenght = baseLenght * 2;
        laser.GetComponent<LaserAttack>().timeSpan = lenghtAtk;
        laser.GetComponent<LaserAttack>().endDelay = lenghtAtk / 2.0f;

        laser.GetComponent<LaserAttack>().followEntity = forceUnFollow;


        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(laser, new DamageInfo(self, baseAtk * dmg * 2, new Velocity(0.125f * dmg, angle)));

        GameObject attack = new GameObject();
        attack.AddComponent<CompositeAttack>();
        attack.GetComponent<CompositeAttack>().attackList.Add(laser.GetComponent<LaserAttack>());

        for (float startDelay = lenghtAtk; startDelay <  1.0f; startDelay += lenghtAtk)
        {

            GameObject newLaser = GameObject.Instantiate(laser);
            newLaser.GetComponent<LaserAttack>().startDelay = startDelay;

            attack.GetComponent<CompositeAttack>().attackList.Add(newLaser.GetComponent<LaserAttack>());
        }

        return attack.GetComponent<CompositeAttack>();
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
