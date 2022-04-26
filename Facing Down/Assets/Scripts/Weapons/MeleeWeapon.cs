using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    protected float baseRange = 3.0f;
    protected float baseLenght = 0f;

    public MeleeWeapon(string target) : base(target)
    {

    }
}
