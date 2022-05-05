using UnityEngine;

public class PlayerDash : AbstractPlayer
{
    private PlayerBulletTime bulletTime;
    private CameraManager camManager;
    private DirectionPointer pointer;
    private Entity self;
    private StatPlayer stat;

    private float chargeTimePassed = 0.0f;
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

        stat = gameObject.GetComponent<Player>().stat;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ComputeDash();
    }

    private void ComputeDash()
    {
        chargeTimePassed += Time.fixedDeltaTime;

        if (Game.controller.IsMovementHeld() && !movePressed)
        {
            movePressed = true;
            chargeTimePassed = 0;
        }
        else if (!Game.controller.IsMovementHeld() && movePressed)
        {
            movePressed = false;

            if (bulletTime.isInBulletTime)
            {
                ComputeRedirect();

                bulletTime.isInBulletTime = false;
                Game.time.SetGameSpeedInstant(2.0f);
            }
            else
            {
                if (chargeTimePassed > chargeTime)
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
            camManager.SetZoomPercent(Mathf.Min(130.0f, 100 + 30 * (chargeTimePassed / chargeTime)));
    }

    private void ComputeMegaDash()
    {
        if (stat.GetRemainingDashes() <= 0)
            return;

        stat.UseDashes(2);
        if (!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(1.6f);

        Game.player.inventory.OnMegaDash();

        self.GetComponent<Rigidbody2D>().velocity = new Velocity(stat.getAcceleration() * 1.5f, pointer.getAngle()).GetAsVector2();
    }

    private void ComputeSimpleDash()
    {
        if (stat.GetRemainingDashes() <= 0)
            return;

        stat.UseDashes(1);
        if (!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(1.2f);

        Game.player.inventory.OnDash();

        self.GetComponent<Rigidbody2D>().velocity = new Velocity(stat.getAcceleration(), pointer.getAngle()).GetAsVector2();
    }

    private void ComputeRedirect()
    {
        Game.player.inventory.OnRedirect();

        Velocity newVelo = new Velocity(self.GetComponent<Rigidbody2D>().velocity);
        newVelo.setAngle(pointer.getAngle());

        self.GetComponent<Rigidbody2D>().velocity = newVelo.GetAsVector2();
    }
}
