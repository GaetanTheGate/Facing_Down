using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    protected float baseAtk;
    protected float baseRange;
    protected float baseLenght;
    protected float baseSpan;
    protected float baseCooldown;

    protected string attackPath;
    protected string specialPath;

    public abstract void Attack(float angle, Entity self);
    public abstract void Special(float angle, Entity self);

    public float GetBaseCooldown()
    {
        return baseCooldown;
    }
}
