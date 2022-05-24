using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    private static float baseHitCooldown = 2.0f;

    public bool isMelee = false;
    public Velocity knockback;
    public Entity source;
    public Entity target;
    public float amount;
    public DamageType type;
    public float hitCooldown;

    public DamageInfo(Entity source, Entity target, float amount, DamageType type, Velocity knockback, float hitCooldown)
    {
        //Debug.Log(source + " hit " + target + " for " + amount + " damage with a " + (type == DamageType.PRIMARY ? "primary" : "secondary") + " attack.");
        this.source = source;
        this.target = target;
        this.amount = amount;
        this.type = type;
        this.knockback = knockback;
        this.hitCooldown = hitCooldown;
    }

    public DamageInfo(Entity source, Entity target, float amount, DamageType type, Velocity knockback) : this(source, target, amount, type, knockback, baseHitCooldown) { }

    public DamageInfo(Entity source, Entity target, float amount, DamageType type) : this(source, target, amount, type, new Velocity(), baseHitCooldown) { }

    public DamageInfo(Entity source, Entity target, float amount) : this(source, target, amount, DamageType.PRIMARY, new Velocity(), baseHitCooldown) { }

    public DamageInfo(Entity source, Entity target, float amount, Velocity knockback) : this(source, target, amount, DamageType.PRIMARY, knockback, baseHitCooldown) { }

    public DamageInfo(Entity source, float amount, Velocity knockback) : this(source, null, amount, DamageType.PRIMARY, knockback, baseHitCooldown) { }

    public DamageInfo(Entity source, float amount, Velocity knockback, float hitCooldown) : this(source, null, amount, DamageType.PRIMARY, knockback, hitCooldown) { }

    public DamageInfo(Entity source, float amount) : this(source, null, amount, DamageType.PRIMARY, new Velocity(), baseHitCooldown) { }

    public DamageInfo(DamageInfo dmgInfo) : this(dmgInfo.source, dmgInfo.target, dmgInfo.amount, dmgInfo.type, dmgInfo.knockback, dmgInfo.hitCooldown) { }

    public DamageInfo() : this(null, null, 0, DamageType.PRIMARY, null, baseHitCooldown) { }
}

public enum DamageType {
    PRIMARY, SECONDARY
}