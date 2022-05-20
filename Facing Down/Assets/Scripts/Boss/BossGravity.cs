using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGravity : MonoBehaviour
{
    bool canChangeGravity = true;

    void FixedUpdate()
    {
        if(canChangeGravity) Game.player.self.GetComponent<GravityEntity>().gravity.setAngle(Angles.AngleBetweenVector2(gameObject.transform.position, Game.player.self.transform.position));
    }

    public void setPlayerNormalGravity()
    {
        canChangeGravity = false;
        Game.player.self.GetComponent<GravityEntity>().gravity.setAngle(270);
    }
}
