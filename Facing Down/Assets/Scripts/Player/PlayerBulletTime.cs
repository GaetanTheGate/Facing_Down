using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerBulletTime : AbstractPlayer, InputListener
{
    public bool isInBulletTime = false;

    //private float startTime;
    public float duration = 0.25f;
    private float count = 0;

    public bool canBulletTime = true;

    public Volume volume;

    private void Start()
    {
        volume = GameObject.Find("Game").GetComponent<Volume>();
    }

    protected override void Initialize()
    {
        Game.controller.Subscribe("BulletTime", this);
    }

    public void OnInputPressed() {
        if (!canBulletTime) return;
        if (!isInBulletTime) {
            ActivateBulletTime();
		}
	}

    public void OnInputReleased() {
        if (!canBulletTime) return;
        if (isInBulletTime) {
            Game.player.inventory.OnBullettimeEnd();
            EndBulletTime();
		}
	}

    public void OnInputHeld() {
        if (!canBulletTime) return;
    }

	public void UpdateAfterInput() {
        //if (!canBulletTime) return;
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
        if (!canBulletTime) return;
        //startTime = Time.time;
        Game.player.inventory.OnBullettimeActivate();
        isInBulletTime = true;
    }

    public void EndBulletTime() {
        if (!canBulletTime) return;
        isInBulletTime = false;
    }

    void Update()
    {
        if (isInBulletTime)
        {
            count += 0.01f;
            count = Mathf.Min(duration, count);
        }
        else
        {
            count -= 0.01f;
            count = Mathf.Max(0, count);
        }

        volume.profile.TryGet<Vignette>(out Vignette vignette);
        vignette.intensity.value = 0.5f * (count / duration);
    }
}
