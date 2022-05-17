using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item
{
    protected string target;

    public Weapon(string target, string id) : base(id) => this.target = target;

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

    public virtual void _Move(float angle, Entity self)
    {
        Velocity newVelo = new Velocity(self.GetComponent<Rigidbody2D>().velocity);
        newVelo.setAngle(angle);

        self.GetComponent<Rigidbody2D>().velocity = newVelo.GetAsVector2();
    }

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


    public void Movement(float angle, Entity self)
    {
        startPos = self.transform.position;
        _Move(angle, self);
    }

    public float GetCooldown()
    {
        return baseSDelay + baseSpan + baseEDelay + baseCooldown;
    }

    public bool IsAuto() => isAuto;

    public bool CanAttack() => canAttack;

    public string getAttackPath() => attackPath;
    public string getSpecialPath() => specialPath;

    public float getSDelay() => baseSDelay;
    public float getEDelay() => baseEDelay;
    public float getSpan() => baseSpan;
    public float getBaseCooldown() => baseCooldown;

    public void SetBaseAtk(float newAtk) {
        baseAtk = newAtk;
    }

    protected void AddHitAttack(GameObject gameObject, DamageInfo dmgInfo)
    {
        gameObject.AddComponent<AttackHit>();
        gameObject.GetComponent<AttackHit>().dmgInfo = dmgInfo;
        gameObject.GetComponent<AttackHit>().layersToHit.Add(target);
    }

    // Item code

    public override string GetSpriteFolderPath()
    {
        return "Sprites/Items/Weapons/";
    }

    public override string GetDescription()
    {
        return description.DESCRIPTION;
    }

    public override Item MakeCopy()
    {
        System.Type type = GetType();
        
        Weapon newWeapon = (Weapon) type.GetConstructor(new System.Type[0]).Invoke(new Object[0]);
        newWeapon.target = target;

        return newWeapon;
    }
}