using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : MeleeAttack
{
    public override Vector3 Behaviour(float percentage)
    {
        transform.localScale = new Vector3(range * percentage, range * percentage, transform.localScale.z);

        return new Vector3(0,0,0);
    }
}
