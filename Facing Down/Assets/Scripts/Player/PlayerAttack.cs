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
    private Entity self;
    private StatPlayer stat;

    private bool attackPressed = false;

    public Weapon weapon;
    public EnumWeapon.WeaponChoice weaponChosen;

    public override void Init()
    {
        camManager = gameObject.GetComponent<Player>().gameCamera.GetComponent<CameraManager>();
        if (camManager == null)
            camManager = gameObject.GetComponent<Player>().gameCamera.gameObject.AddComponent<CameraManager>();

        bulletTime = gameObject.GetComponent<PlayerBulletTime>();
        if (bulletTime == null)
            bulletTime = gameObject.AddComponent<PlayerBulletTime>();


        self = gameObject.GetComponent<Player>().self;
        pointer = gameObject.GetComponent<Player>().pointer;

        stat = gameObject.GetComponent<Player>().stat;
    }

    void FixedUpdate()
    {
        if (weapon == null || ! weapon.GetType().Equals(EnumWeapon.GetWeaponType(weaponChosen)))
        {
            weapon = EnumWeapon.GetWeapon(weaponChosen, "Enemy");
        }
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
            if(weapon.IsAuto()) ComputeSimpleAttack();

            Game.controller.lowSensitivity = true;
            camManager.SetZoomPercent(Mathf.Max(90.0f, 100 - 10 * (chargeTimePassed / chargeTime)));
            if (!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(0.6f);
        }
    }

    private void ComputeSimpleAttack()
    {
        if (attackRecharge < weapon.GetCooldown() ||  ! weapon.CanAttack())
            return;

        attackRecharge = 0.0f;
        if (!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(0.2f);

        weapon.Attack(pointer.getAngle(), self);
    }

    private void ComputeBounce()
    {

    }
    private void ComputeSpecial()
    {
        if ( ! weapon.CanAttack())
            return;

        bulletTime.isInBulletTime = false;
        weapon.Special(pointer.getAngle(), self);
        if(!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(0.1f);
    }
}
