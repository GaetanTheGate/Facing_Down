using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AbstractPlayer, InputListener
{

    private float chargeTimePassed = 0.0f;
    private float chargeTime = 1.0f;

    private float attackRecharge = 0.0f;

    private PlayerBulletTime bulletTime;
    private CameraManager camManager;
    private DirectionPointer pointer;
    private Player self;
    private Entity selfEntity;
    private RotationEntity rotation;

    protected override void Initialize()
    {
        self = gameObject.GetComponent<Player>();
        if (self == null)
        {
            self = gameObject.AddComponent<Player>();
            self.Init();
        }

        camManager = self.gameCamera.GetComponent<CameraManager>();
        if (camManager == null)
            camManager = self.gameCamera.gameObject.AddComponent<CameraManager>();

        bulletTime = gameObject.GetComponent<PlayerBulletTime>();
        if (bulletTime == null)
        {
            bulletTime = gameObject.AddComponent<PlayerBulletTime>();
            bulletTime.Init();
        }


        selfEntity = self.self;
        pointer = self.pointer;



        rotation = selfEntity.GetComponent<RotationEntity>();
        if (rotation == null)
        {
            rotation = selfEntity.gameObject.AddComponent<RotationEntity>();
            rotation.Init();
        }

        Game.controller.Subscribe(Options.Get().dicoCommandsKeyBoard["attack"], this);
    }

    public void OnInputPressed () {
        chargeTimePassed = 0;
    }

    public void OnInputHeld() {
        if (self.inventory.GetWeapon().IsAuto()) {
            ComputeSimpleAttack();
        }

        Game.controller.lowSensitivity = true;
        camManager.SetZoomPercent(Mathf.Max(90.0f, 100 - 10 * (chargeTimePassed / chargeTime)));
        if (!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(0.6f);
    }

    public void OnInputReleased() {
        chargeTimePassed = 0;
        Game.controller.lowSensitivity = false;

        if (bulletTime.isInBulletTime) {
            ComputeSpecial();
        }
        else {
            ComputeSimpleAttack();
        }

        camManager.SetZoomPercent(100);
    }

    public void UpdateAfterInput() {
        attackRecharge += Time.fixedDeltaTime;
        chargeTimePassed += Time.fixedDeltaTime;
    }

    private void ComputeSimpleAttack()
    {
        if (attackRecharge < self.inventory.GetWeapon().GetCooldown() ||  !self.inventory.GetWeapon().CanAttack())
            return;

        attackRecharge = 0.0f;
        if (!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(0.2f);

        self.inventory.GetWeapon().Attack(pointer.getAngle(), selfEntity);

        rotation.FlipEntityRelativeToGravity(pointer.getAngle());
    }

    private void ComputeSpecial()
    {
        if ( !self.inventory.GetWeapon().CanSpecial())
            return;
        if (Game.player.stat.GetSpecialLeft() < 3) return;
        Game.player.stat.ModifySpecialLeft(-3);

        bulletTime.isInBulletTime = false;
        self.inventory.GetWeapon().Special(pointer.getAngle(), selfEntity);
        if(!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(0.1f);

        rotation.FlipEntityRelativeToGravity(pointer.getAngle());
    }
}
