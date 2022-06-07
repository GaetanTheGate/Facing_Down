using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCyborgDeath : EnemyDeath
{
    public override void die()
    {
        base.die();
        explode();
        Destroy(parentToDestroy);
    }
}
