using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileWeapon : Weapon
{
    protected float baseSpeed;

    public ProjectileWeapon(string target, string id) : base(target, id)
    {

    }
}