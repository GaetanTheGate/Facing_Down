using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AbstractPlayer
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

    private bool attackPressed = false;

    public override void Init()
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

    }

    void FixedUpdate()
    {
        ComputeAttack();
    }

    private void ComputeAttack()
    {
        attackRecharge += Time.fixedDeltaTime;
        chargeTimePassed += Time.fixedDeltaTime;

        

        if (Game.controller.IsAttackHeld() && !attackPressed)
        {
            attackPressed = true;
            chargeTimePassed = 0;
        }
        else if (!Game.controller.IsAttackHeld() && attackPressed)
        {
            attackPressed = false;
            chargeTimePassed = 0;
            Game.controller.lowSensitivity = false;

            if (bulletTime.isInBulletTime)
            {
                ComputeSpecial();
            }
            else
            {
                ComputeSimpleAttack();
            }

            camManager.SetZoomPercent(100);
        }
        else if (attackPressed)
        {
            if (self.inventory.GetWeapon().IsAuto())
            {
                ComputeSimpleAttack();
            }

            Game.controller.lowSensitivity = true;
            camManager.SetZoomPercent(Mathf.Max(90.0f, 100 - 10 * (chargeTimePassed / chargeTime)));
            if (!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(0.6f);
        }
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

    private void ComputeBounce()
    {

    }
    private void ComputeSpecial()
    {
        if ( !self.inventory.GetWeapon().CanAttack())
            return;

        bulletTime.isInBulletTime = false;
        self.inventory.GetWeapon().Special(pointer.getAngle(), selfEntity);
        if(!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(0.1f);

        rotation.FlipEntityRelativeToGravity(pointer.getAngle());
    }
}
