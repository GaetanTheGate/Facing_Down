using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DammageInfo
{
    public Entity source;
    public Entity target;
    public float amount;
    public DamageType type;

    public DammageInfo(Entity source, Entity target, float amount, DamageType type) {
        this.source = source;
        this.target = target;
        this.amount = amount;
        this.type = type;
	}

    public DammageInfo(Entity source, Entity target, float amount) : this(source, target, amount, DamageType.PRIMARY) { }
}

public enum DamageType {
    PRIMARY, SECONDARY
}