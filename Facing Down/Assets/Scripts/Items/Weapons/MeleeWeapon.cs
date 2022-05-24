using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    protected float baseRange = 3.0f;
    protected float baseLenght = 0f;

    public void SetBaseRange(float range) => baseRange = range;
    public void SetBaseLenght(float lenght) => baseLenght = lenght;

    public MeleeWeapon(string target, string id) : base(target, id)
    {

    }
}
