using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCyborgDeath : EnemyDeath
{
    public override void die()
    {
        base.die();
        animator.SetBool("dead", true);
        Destroy(parentToDestroy, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
