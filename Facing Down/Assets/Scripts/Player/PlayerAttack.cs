using UnityEngine;

public class PlayerAttack : AbstractPlayer
{
    private float attackCooldown = 0.5f;
    private float lastAttack = 0.0f;

    private float chargeTimeStart = 0.0f;
    private float chargeTime = 1.0f;

    private PlayerBulletTime bulletTime;
    private CameraManager camManager;
    private DirectionPointer pointer;
    private Entity self;
    private StatPlayer stat;

    private bool attackPressed = false;

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
        ComputeAttack();
    }

    private void ComputeAttack()
    {
        if (Time.realtimeSinceStartup - lastAttack < attackCooldown)
            return;

        if (Game.controller.IsAttackHeld() && !attackPressed)
        {
            attackPressed = true;
            chargeTimeStart = Time.realtimeSinceStartup;
        }
        else if (!Game.controller.IsAttackHeld() && attackPressed)
        {
            attackPressed = false;
            lastAttack = Time.realtimeSinceStartup;

            if (bulletTime.isInBulletTime)
            {
                ComputeSpecial();

                bulletTime.isInBulletTime = false;
                Game.time.SetGameSpeedInstant(2.0f);
            }
            else
            {
                if (Time.realtimeSinceStartup - chargeTimeStart > chargeTime)
                {
                    ComputeBounce();
                    Game.time.SetGameSpeedInstant(1.6f);
                }
                else
                {
                    ComputeSimpleAttack();
                    Game.time.SetGameSpeedInstant(1.2f);
                }

            }
            camManager.SetZoomPercent(100);
        }
        else if (attackPressed)
            camManager.SetZoomPercent(Mathf.Min(130.0f, 100 - 30 * ((Time.realtimeSinceStartup - chargeTimeStart) / chargeTime)));
    }

    private void ComputeSimpleAttack()
    {

    }

    private void ComputeBounce()
    {

    }
    private void ComputeSpecial()
    {

    }
}
