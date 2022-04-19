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

    public GameObject attack;

    [Min(0)] public float lenght = 270;
    [Min(0)] public float range = 3;

    [Min(0)] public float attackCooldown = 0.5f;
    public Attack.Way behaviour = Attack.Way.Clockwise;

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
        attackRecharge += Time.fixedDeltaTime;
        chargeTimePassed += Time.fixedDeltaTime;

        if (attackRecharge < attackCooldown)
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
        attack.gameObject.transform.position = self.transform.position;
        attack.gameObject.GetComponent<Attack>().src = self;
        attack.gameObject.GetComponent<Attack>().angle = pointer.getAngle();
        attack.gameObject.GetComponent<Attack>().range = range;
        attack.gameObject.GetComponent<Attack>().lenght = lenght;
        attack.gameObject.GetComponent<Attack>().timeSpan = attackCooldown/2;
        attack.gameObject.GetComponent<Attack>().color = Color.white;
        attack.gameObject.GetComponent<Attack>().behaviour = behaviour;
        attack.gameObject.GetComponent<Attack>().followEntity = true;

        Instantiate(attack).GetComponent<Attack>().startAttack();

        if (behaviour == Attack.Way.Clockwise)
            behaviour = Attack.Way.CounterClockwise;
        else if (behaviour == Attack.Way.CounterClockwise)
            behaviour = Attack.Way.Clockwise;
    }

    private void ComputeBounce()
    {

    }
    private void ComputeSpecial()
    {
        attack.gameObject.transform.position = self.transform.position;
        attack.gameObject.GetComponent<Attack>().src = self;
        attack.gameObject.GetComponent<Attack>().angle = pointer.getAngle();
        attack.gameObject.GetComponent<Attack>().range = 7;
        attack.gameObject.GetComponent<Attack>().lenght = 360;
        attack.gameObject.GetComponent<Attack>().timeSpan = 2;
        attack.gameObject.GetComponent<Attack>().color = Color.red;
        attack.gameObject.GetComponent<Attack>().behaviour = behaviour;
        attack.gameObject.GetComponent<Attack>().followEntity = false;

        Instantiate(attack).GetComponent<Attack>().startAttack();

        if (behaviour == Attack.Way.Clockwise)
            behaviour = Attack.Way.CounterClockwise;
        else if (behaviour == Attack.Way.CounterClockwise)
            behaviour = Attack.Way.Clockwise;

        attack.gameObject.GetComponent<Attack>().behaviour = behaviour;
        Instantiate(attack).GetComponent<Attack>().startAttack();
    }
}
