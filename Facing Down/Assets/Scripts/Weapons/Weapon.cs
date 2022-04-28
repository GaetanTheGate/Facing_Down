using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    protected string target;

    public Weapon(string target) => this.target = target;

    protected float baseAtk = 100.0f;
    protected float baseSpan = 1.0f;
    protected float baseSDelay = 0.0f;
    protected float baseEDelay = 0.0f;
    protected float baseCooldown = 0.1f;

    protected string attackPath = "Prefabs/Weapons/Katana";
    protected string specialPath = "Prefabs/Weapons/Katana";

    protected bool isAuto = false;
    protected bool canAttack = true;

    public bool forceUnFollow = true;
    public Vector3 startPos;

    public abstract void WeaponAttack(float angle, Entity self);
    public abstract void WeaponSpecial(float angle, Entity self);

    public abstract Attack GetAttack(float angle, Entity self);
    public abstract Attack GetSpecial(float angle, Entity self);

    public void Attack(float angle, Entity self)
    {
        startPos = self.transform.position;
        WeaponAttack(angle, self);
    }

    public void Special(float angle, Entity self)
    {
        startPos = self.transform.position;
        WeaponSpecial(angle, self);
    }

    public float GetCooldown()
    {
        return baseSDelay + baseSpan + baseEDelay + baseCooldown;
    }

    public bool IsAuto() => isAuto ? true : false;

    public bool CanAttack() => canAttack ? true : false;

    public string getAttackPath() => attackPath;
    public string getSpecialPath() => specialPath;

    public float getSDelay() => baseSDelay;
    public float getEDelay() => baseEDelay;
    public float getSpan() => baseSpan;
    public float getBaseCooldown() => baseCooldown;

    protected void AddHitAttack(GameObject gameObject, DamageInfo dmgInfo)
    {
        gameObject.AddComponent<AttackHit>();
        gameObject.GetComponent<AttackHit>().dmgInfo = dmgInfo;
        gameObject.GetComponent<AttackHit>().layersToHit.Add(target);
    }
}
