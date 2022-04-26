using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public Entity source;
    public Entity target;
    public float amount;
    public DamageType type;

    public DamageInfo(Entity source, Entity target, float amount, DamageType type) {
        Debug.Log(source + " hit " + target + " for " + amount + " damage with a " + (type == DamageType.PRIMARY ? "primary" : "secondary") + " attack.");
        this.source = source;
        this.target = target;
        this.amount = amount;
        this.type = type;
	}

    public DamageInfo(Entity source, Entity target, float amount) : this(source, target, amount, DamageType.PRIMARY) { }
}

public enum DamageType {
    PRIMARY, SECONDARY
}