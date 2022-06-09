using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntityCollisionStructure : EntityCollisionStructure
{
    /*public override void checkCollisionOnStay(Collision2D col)
    {
        base.checkCollisionOnStay(col);
        PlayerOnCollisionStructure playerOnCollisionStructure = gameObject.GetComponentInParent<PlayerOnCollisionStructure>();
        if (playerOnCollisionStructure != null) playerOnCollisionStructure.onStayCollide();
    }*/
}
