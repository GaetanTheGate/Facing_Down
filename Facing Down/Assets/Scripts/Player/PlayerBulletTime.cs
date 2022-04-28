using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletTime : AbstractPlayer
{
    private bool bulletTimePressed = false;

    public bool isInBulletTime = false;

    public override void Init()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ComputeBulletTime();
    }


    private void ComputeBulletTime()
    {
        if (Game.controller.IsBulletTimeHeld() && !bulletTimePressed)
        {
            bulletTimePressed = true;
            isInBulletTime = true;
            Game.player.inventory.OnBullettimeActivate();
        }
        else if (!Game.controller.IsBulletTimeHeld() && bulletTimePressed)
        {
            if (isInBulletTime) Game.player.inventory.OnBullettimeEnd();
            bulletTimePressed = false;
            isInBulletTime = false;
        }

        if (isInBulletTime)
            Game.time.SetGameSpeed(0.00f);
        else
            Game.time.SetGameSpeed(1.0f);
    }
}
