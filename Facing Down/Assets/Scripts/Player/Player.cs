using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isInBulletTime = false;

    public StatEntity stat;
    private CameraManager camManager;
    public Entity self;
    public DirectionPointer pointer;
    public Camera gameCamera;
    public Inventory inventory;

    public int dashLeft = 2;


    private float chargeTimeStart = 0.0f;
    private float chargeTime = 1.0f;

    private bool bulletTimePressed = false;

    private bool attackPressed = false;
    private bool movePressed = false;

    // Start is called before the first frame update
    void Start()
    {
        stat = self.GetComponent<StatEntity>();
        if (stat == null)
            stat = self.gameObject.AddComponent<StatEntity>();

        camManager = gameCamera.GetComponent<CameraManager>();
        if (camManager == null)
            camManager = gameCamera.gameObject.AddComponent<CameraManager>();

        inventory = self.GetComponent<Inventory>();
        if (inventory == null)
            inventory = self.gameObject.AddComponent<Inventory>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ComputeBulletTime();
        ComputeDash();
        ComputeMaterial();
    }

    private void ComputeMaterial()
    {
        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.bounciness = 0;
        if (new Velocity(self.GetRigidBody().velocity).getSpeed() > stat.maxSpeed / 3)
            material.friction = 0.1f;
        else
            material.friction = 0.6f;

        self.GetComponent<BoxCollider2D>().sharedMaterial = material;
    }

    private void ComputeBulletTime()
    {
        if(Game.controller.IsBulletTimeHeld() && !bulletTimePressed)
        {
            bulletTimePressed = true;
            isInBulletTime = true;
        }
        else if ( ! Game.controller.IsBulletTimeHeld() && bulletTimePressed)
        {

            bulletTimePressed = false;
            isInBulletTime = false;
        }

        if (isInBulletTime)
            Game.time.SetGameSpeed(0.05f);
        else
            Game.time.SetGameSpeed(1.0f);
    }

    private void ComputeDash()
    {
        if (dashLeft <= 0)
            return;

        if (Game.controller.IsMovementHeld() && !movePressed)
        {
            movePressed = true;
            chargeTimeStart = Time.realtimeSinceStartup;
        }
        else if (!Game.controller.IsMovementHeld() && movePressed)
        {
            movePressed = false;
            dashLeft -= 1;

            if (isInBulletTime)
            {
                ComputeRedirect();

                isInBulletTime = false;
                Game.time.SetGameSpeedInstant(2.0f);
            }
            else
            {
                if (Time.realtimeSinceStartup - chargeTimeStart > chargeTime)
                {
                    ComputeChargedDash();
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
        else if(movePressed)
            camManager.SetZoomPercent(Mathf.Min(130.0f, 100 + 30 * ((Time.realtimeSinceStartup - chargeTimeStart) / chargeTime)));
    }

    private void ComputeChargedDash()
    {
        self.GetRigidBody().velocity = new Velocity(stat.acceleration * 3, pointer.getAngle()).GetAsVector2();
    }

    private void ComputeSimpleDash()
    {
        self.GetRigidBody().velocity = new Velocity(stat.acceleration, pointer.getAngle()).GetAsVector2();
    }

    private void ComputeRedirect()
    {
        Velocity newVelo = new Velocity(self.GetRigidBody().velocity);
        newVelo.setAngle(pointer.getAngle());

        self.GetRigidBody().velocity = newVelo.GetAsVector2();
    }
}
