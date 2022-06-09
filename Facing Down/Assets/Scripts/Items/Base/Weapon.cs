using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item {
    protected string target;

    public AudioClip attackAudio;
    public AudioClip specialAudio;

    public Weapon(string target, string id) : base(id) { 
        this.target = target;
        //this.stat = ; 
    }

    protected float baseAtk = 100.0f;
    protected float baseSpan = 1.0f;
    protected float baseSDelay = 0.0f;
    protected float baseEDelay = 0.0f;
    protected float baseCooldown = 0.1f;

    public WeaponStat stat = new WeaponStat();
    protected DamageType dmgType = DamageType.PRIMARY;

    protected string attackPath = "Prefabs/Weapons/Katana";
    protected string specialPath = "Prefabs/Weapons/Katana";

    protected bool isAuto = false;
    protected bool canAttack = true;
    protected bool canSpecial = true;
    protected bool canMove = true;

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
    public bool CanSpecial() => canSpecial;
    public bool CanMove() => canMove;

    public string getAttackPath() => attackPath;
    public string getSpecialPath() => specialPath;

    public float getSDelay() => baseSDelay;
    public float getEDelay() => baseEDelay;
    public float getSpan() => baseSpan;
    public float getBaseCooldown() => baseCooldown;

    public void SetBaseAtk(float newAtk) => baseAtk = newAtk;
    public void SetBaseSDelay(float span) => baseSDelay = span;
    public void SetBaseSpan(float span) => baseSpan = span;
    public void SetBaseEDelay(float span) => baseEDelay = span;
    public void SetBaseCooldown(float cooldown) => baseCooldown = cooldown;
    public void SetDmgType(DamageType type) => dmgType = type;

    protected float GetBaseDmg(Entity self) => self.GetComponent<StatEntity>().GetComputedDmg() / 100 * baseAtk;
    protected float GetKnockbackIntensity(Entity self, float knockIntensity) => self.GetComponent<StatEntity>().getAtk() / 100 * knockIntensity;

    protected virtual void AddHitAttack(GameObject gameObject, DamageInfo dmgInfo)
    {
        gameObject.AddComponent<AttackHit>();
        gameObject.GetComponent<AttackHit>().dmgInfo = dmgInfo;
        gameObject.GetComponent<AttackHit>().dmgInfo.type = dmgType;
        gameObject.GetComponent<AttackHit>().layersToHit.Add(target);
    }

    // Item code

    public override string GetSpriteFolderPath()
    {
        return "Sprites/Items/Weapons/";
    }

    protected override void InitDescription(string id) {
        this.description = Localization.GetWeaponDescription(id);
    }

    public override string GetDescription()
    {
        return description.DESCRIPTION;
    }

    public override Item MakeCopy()
    {
        System.Type type = GetType();
        
        Weapon newWeapon = (Weapon) type.GetConstructor(new System.Type[] { typeof(string) }).Invoke(new object[] { target });

        return newWeapon;
    }
}
