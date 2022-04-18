using UnityEngine;

public class PlayerDash : AbstractPlayer
{
    private PlayerBulletTime bulletTime;
    private CameraManager camManager;
    private DirectionPointer pointer;
    private Entity self;
    private StatPlayer stat;

    private float chargeTimeStart = 0.0f;
    private float chargeTime = 1.0f;
    private bool movePressed = false;

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
            stat = gameObject.AddComponent<StatPlayer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ComputeDash();
    }

    private void ComputeDash()
    {
        if (stat.numberOfDashes >= stat.maxDashes)
            return;

        if (Game.controller.IsMovementHeld() && !movePressed)
        {
            movePressed = true;
            chargeTimeStart = Time.realtimeSinceStartup;
        }
        else if (!Game.controller.IsMovementHeld() && movePressed)
        {
            movePressed = false;
            stat.numberOfDashes += 1;

            if (bulletTime.isInBulletTime)
            {
                ComputeRedirect();

                bulletTime.isInBulletTime = false;
                Game.time.SetGameSpeedInstant(2.0f);
            }
            else
            {
                if (Time.realtimeSinceStartup - chargeTimeStart > chargeTime)
                {
                    ComputeMegaDash();
                    Game.time.SetGameSpeedInstant(1.6f);
                }
                else
                {
                    ComputeSimpleDash();
                    Game.time.SetGameSpeedInstant(1.2f);
                }

            }
            camManager.SetZoomPercent(100);
        }
        else if (movePressed)
            camManager.SetZoomPercent(Mathf.Min(130.0f, 100 + 30 * ((Time.realtimeSinceStartup - chargeTimeStart) / chargeTime)));
    }

    private void ComputeMegaDash()
    {
        self.GetComponent<Rigidbody2D>().velocity = new Velocity(stat.statEntity.acceleration * 2, pointer.getAngle()).GetAsVector2();
    }

    private void ComputeSimpleDash()
    {
        self.GetComponent<Rigidbody2D>().velocity = new Velocity(stat.statEntity.acceleration * 1.25f, pointer.getAngle()).GetAsVector2();
    }

    private void ComputeRedirect()
    {
        Velocity newVelo = new Velocity(self.GetComponent<Rigidbody2D>().velocity);
        newVelo.setAngle(pointer.getAngle());

        self.GetComponent<Rigidbody2D>().velocity = newVelo.GetAsVector2();
    }
}
