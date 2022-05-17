using UnityEngine;

public class PlayerDash : AbstractPlayer
{
    private PlayerBulletTime bulletTime;
    private CameraManager camManager;
    private DirectionPointer pointer;
    private Player player;
    private Entity self;
    private StatPlayer stat;
    private Rigidbody2D rb;
    private GravityEntity gravity;

    private float chargeTimePassed = 0.0f;
    private float chargeTime = 1.0f;
    private bool movePressed = false;

    public override void Init()
    {
        player = gameObject.GetComponent<Player>();
        if (player == null)
        {
            player = gameObject.AddComponent<Player>();
            player.Init();
        }

        camManager = player.gameCamera.GetComponent<CameraManager>();
        if (camManager == null)
            camManager = player.gameCamera.gameObject.AddComponent<CameraManager>();

        bulletTime = gameObject.GetComponent<PlayerBulletTime>();
        if (bulletTime == null)
        {
            bulletTime = gameObject.AddComponent<PlayerBulletTime>();
            bulletTime.Init();
        }

        

        self = player.self;
        pointer = player.pointer;

        stat = player.stat;


        rb = Entity.initRigidBody(self.gameObject);

        gravity = self.GetComponent<GravityEntity>();
        if (gravity == null)
        {
            gravity = self.gameObject.AddComponent<GravityEntity>();
            gravity.Init();
        }
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
            float angleDirection = new Velocity(1, pointer.getAngle()).SubToAngle(gravity.gravity.getAngle()).getAngle();
            if (angleDirection > 180 && angleDirection <= 360)
                self.transform.localScale = new Vector3(-1 * Mathf.Abs(self.transform.localScale.x), self.transform.localScale.y, self.transform.localScale.z);
            else
                self.transform.localScale = new Vector3(Mathf.Abs(self.transform.localScale.x), self.transform.localScale.y, self.transform.localScale.z);

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

        player.inventory.OnMegaDash();

        rb.velocity = new Velocity(stat.getAcceleration() * 1.5f, pointer.getAngle()).GetAsVector2();
    }

    private void ComputeSimpleDash()
    {
        if (stat.GetRemainingDashes() <= 0)
            return;

        stat.UseDashes(1);
        if (!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(1.2f);

        player.inventory.OnDash();

        rb.velocity = new Velocity(stat.getAcceleration(), pointer.getAngle()).GetAsVector2();
    }

    private void ComputeRedirect()
    {
        player.inventory.OnRedirect();

        player.inventory.GetWeapon().Movement(pointer.getAngle(), self);
    }
}
