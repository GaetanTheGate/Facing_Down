using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    public int id;

    public Effect(int id)
    {
        this.id = id;
    }

    public abstract void OnHit(DamageInfo damageInfo);
}
