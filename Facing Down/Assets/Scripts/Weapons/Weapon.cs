using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    protected float baseAtk = 100.0f;
    protected float baseRange = 3.0f;
    protected float baseLenght = 0f;
    protected float baseSpan = 0.5f;
    protected float baseSDelay = 0.0f;
    protected float baseEDelay = 0.0f;
    protected float baseCooldown = 0.1f;

    protected string attackPath = "Prefabs/Weapons/Katana";
    protected string specialPath = "Prefabs/Weapons/Katana";

    protected bool isAuto = false;
    protected bool canAttack = true;

    public abstract void Attack(float angle, Entity self);
    public abstract void Special(float angle, Entity self);

    public float GetBaseCooldown()
    {
        return baseSDelay + baseSpan + baseEDelay +baseCooldown;
    }

    public bool IsAuto() => isAuto ? true : false;

    public bool CanAttack() => canAttack ? true : false;
}
