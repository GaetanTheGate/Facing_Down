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

    public Weapon weapon = new Katana();
    public ChooseWeapon weaponChosen = ChooseWeapon.Katana;

    public enum ChooseWeapon
    {
        Katana, Wings
    }

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

        stat = gameObject.GetComponent<StatPlayer>();
        if (stat == null)
            stat = gameObject.AddComponent<StatPlayer>(); ;
    }

    void FixedUpdate()
    {
        switch (weaponChosen)
        {
            case ChooseWeapon.Katana:
                if (weapon.GetType().Equals(typeof(Katana)))
                    break;
                weapon = new Katana();
                break;
            case ChooseWeapon.Wings:
                if (weapon.GetType().Equals(typeof(Wings)))
                    break;
                weapon = new Wings();
                break;
        }
        ComputeAttack();
    }

    private void ComputeAttack()
    {
        attackRecharge += Time.fixedDeltaTime;
        chargeTimePassed += Time.fixedDeltaTime;

        if (attackRecharge < weapon.GetBaseCooldown())
            return;

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

                bulletTime.isInBulletTime = false;
                Game.time.SetGameSpeedInstant(0.1f);
            }
            else
            {
                attackRecharge = 0.0f;
                ComputeSimpleAttack();
                Game.time.SetGameSpeedInstant(0.2f);
            }
            camManager.SetZoomPercent(100);
        }
        else if (attackPressed)
        {
            Game.time.SetGameSpeedInstant(0.5f);
            Game.controller.lowSensitivity = true;
            camManager.SetZoomPercent(Mathf.Max(90.0f, 100 - 10 * (chargeTimePassed / chargeTime)));
        }
    }

    private void ComputeSimpleAttack()
    {
        weapon.Attack(pointer.getAngle(), self);
    }

    private void ComputeBounce()
    {

    }
    private void ComputeSpecial()
    {
        weapon.Special(pointer.getAngle(), self);
    }
}
