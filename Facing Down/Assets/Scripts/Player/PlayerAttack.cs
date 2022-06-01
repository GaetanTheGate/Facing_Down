using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : AbstractPlayer, InputListener
{

    private float chargeTimePassed = 0.0f;
    private float chargeTime = 1.0f;

    private bool isInCooldown = false;

    private PlayerBulletTime bulletTime;
    private CameraManager camManager;
    private DirectionPointer pointer;
    private Player self;
    private Entity selfEntity;
    private RotationEntity rotation;

    public bool canAttack = true;

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

        Game.controller.Subscribe("Attack", this);
    }

    public void OnInputPressed () {
        if (!canAttack) return;
        chargeTimePassed = 0;
    }

    public void OnInputHeld() {
        if (!canAttack) return;
        if (self.inventory.GetWeapon().IsAuto()) {
            ComputeSimpleAttack();
        }

        Game.controller.lowSensitivity = true;
        camManager.SetZoomPercent(Mathf.Max(90.0f, 100 - 10 * (chargeTimePassed / chargeTime)));
        if (!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(0.6f);
    }

    public void OnInputReleased() {
        if (!canAttack) return;
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
        if (!canAttack) return;
        chargeTimePassed += Time.fixedDeltaTime;
    }

    private void ComputeSimpleAttack()
    {
        if (!canAttack) return;
        if (isInCooldown ||  !self.inventory.GetWeapon().CanAttack())
            return;

        StartCoroutine(PreventAttackFor(self.inventory.GetWeapon().GetCooldown()));

        if (!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(0.2f);

        self.inventory.GetWeapon().Attack(pointer.getAngle(), selfEntity);

        rotation.FlipEntityRelativeToGravity(pointer.getAngle());
        camManager.Propulse(pointer.getAngle(), 1f, 2f);
    }

    private void ComputeSpecial()
    {
        if (!canAttack) return;
        if ( !self.inventory.GetWeapon().CanSpecial())
            return;
        if (Game.player.stat.GetSpecialLeft() < 3) return;
        Game.player.stat.ModifySpecialLeft(-3);

        bulletTime.isInBulletTime = false;
        self.inventory.GetWeapon().Special(pointer.getAngle(), selfEntity);
        if(!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(0.1f);

        rotation.FlipEntityRelativeToGravity(pointer.getAngle());
        camManager.Propulse(pointer.getAngle(), 2f, 3f);
    }

    private IEnumerator PreventAttackFor(float duration)
    {
        isInCooldown = true;
        yield return new WaitForSeconds(duration);
        isInCooldown = false;
    }
}
