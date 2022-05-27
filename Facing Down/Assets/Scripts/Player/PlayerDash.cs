using UnityEngine;

public class PlayerDash : AbstractPlayer, InputListener
{
    private PlayerBulletTime bulletTime;
    private CameraManager camManager;
    private DirectionPointer pointer;
    private Player player;
    private Entity self;
    private StatPlayer stat;
    private Rigidbody2D rb;
    private RotationEntity rotation;

    private float chargeTimePassed = 0.0f;
    private float chargeTime = 0.2f;

    protected override void Initialize()
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

        rotation = self.GetComponent<RotationEntity>();
        if (rotation == null)
        {
            rotation = self.gameObject.AddComponent<RotationEntity>();
            rotation.Init();
        }

        Game.controller.Subscribe("dash", this);
    }

    public void OnInputPressed() {
        print("onInputPressed");
        chargeTimePassed = 0;
    }

    public void OnInputHeld() {
        camManager.SetZoomPercent(Mathf.Min(110.0f, 100 + 10 * (chargeTimePassed / chargeTime)));
    }

    public void OnInputReleased() {
        if (bulletTime.isInBulletTime) {
            if (!player.inventory.GetWeapon().CanMove())
                return;

            if (Game.player.stat.GetSpecialLeft() < 1)
                return;
            Game.player.stat.ModifySpecialLeft(-1);

            ComputeSpecialMove();

            rotation.FlipEntityRelativeToGravity(pointer.getAngle());
            rotation.RotateEntityRelativeToFlip(pointer.getAngle());
        }
        else {
            if (stat.GetRemainingDashes() <= 0)
                return;
            else if (chargeTimePassed > chargeTime) {
                ComputeMegaDash();
            }
            else {
                ComputeSimpleDash();
                Game.time.SetGameSpeedInstant(1.2f);
            }

            rotation.FlipEntityRelativeToGravity(pointer.getAngle());
            rotation.RotateEntityRelativeToFlip(pointer.getAngle());

        }

        camManager.SetZoomPercent(100);
    }

    public void UpdateAfterInput() {
        chargeTimePassed += Time.fixedDeltaTime;
    }

    private void ComputeMegaDash()
    {
        stat.UseDashes(2);
        if (!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(1.6f);

        player.inventory.OnMegaDash();

        rb.velocity = new Velocity(stat.GetAcceleration() * 1.5f, pointer.getAngle()).GetAsVector2();

        Game.time.SetGameSpeedInstant(1.6f);
    }

    private void ComputeSimpleDash()
    {
        stat.UseDashes(1);
        if (!bulletTime.isInBulletTime) Game.time.SetGameSpeedInstant(1.2f);

        player.inventory.OnDash();

        rb.velocity = new Velocity(stat.GetAcceleration(), pointer.getAngle()).GetAsVector2();

        Game.time.SetGameSpeedInstant(1.2f);
    }

    private void ComputeSpecialMove()
    {
        player.inventory.OnRedirect();

        player.inventory.GetWeapon().Movement(pointer.getAngle(), self);


        bulletTime.isInBulletTime = false;
        Game.time.SetGameSpeedInstant(2.0f);
    }
}
