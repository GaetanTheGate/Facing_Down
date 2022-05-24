using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletTime : AbstractPlayer, InputListener
{
    public bool isInBulletTime = false;

    private float startTime;

    protected override void Initialize()
    {
        Debug.Log("INIT");
        Game.controller.Subscribe("bulletTime", this);
    }

    public void OnInputPressed() {
        if (!isInBulletTime) {
            ActivateBulletTime();
		}
	}

    public void OnInputReleased() {
        if (isInBulletTime) {
            Game.player.inventory.OnBullettimeEnd();
            EndBulletTime();
		}
	}

    public void OnInputHeld() {

    }

	public void UpdateAfterInput() {
        if (isInBulletTime && Game.player.stat.GetSpecialLeft() == 0) {
            Game.player.inventory.OnBullettimeEnd();
            EndBulletTime();
		}
        if (isInBulletTime) {
            if (Game.time.GetGameSpeed() > 0)
            Game.player.stat.ModifySpecialLeft(- Time.fixedDeltaTime / Game.player.stat.GetSpecialDuration() / Game.time.GetGameSpeed());
            Game.time.SetGameSpeed(0.00f);
        }
        else {
            if (Game.time.GetGameSpeed() > 0)
                Game.player.stat.ModifySpecialLeft(Time.fixedDeltaTime / Game.player.stat.GetSpecialCooldown() / Game.time.GetGameSpeed());
            Game.time.SetGameSpeed(1.0f);
        }
    }

	public void ActivateBulletTime() {
        startTime = Time.time;
        Game.player.inventory.OnBullettimeActivate();
        isInBulletTime = true;
	}

    public void EndBulletTime() {
        isInBulletTime = false;
	}
}
