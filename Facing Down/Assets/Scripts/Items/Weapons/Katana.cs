using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MeleeWeapon
{
    private bool hasBeenGrounded = true;

    public Katana() : this("Enemy") { }
    public Katana(string target) : base(target, "Katana")
    {
        baseAtk = 100;
        baseRange = 3;
        baseLenght = 180;
        baseSpan = 0.2f;
        baseCooldown = 0.0f;

        stat.maxDashes = 3;
        stat.maxSpecial = 4;
        
        stat.accelerationMult = 1.25f;
        stat.specialCooldownMult = 0.75f;

        attackPath = "Prefabs/Items/Weapons/Katana";
        specialPath = "Prefabs/Items/Weapons/KatanaDash";
        attackAudio = Resources.Load<AudioClip>("Sound_Effects/sword_swing");
        specialAudio = Resources.Load<AudioClip>("Sound_Effects/katana_dash");
    }

    private SlashAttack.Way way = SlashAttack.Way.CounterClockwise;

    public override void WeaponAttack(float angle, Entity self)
    {

        GetAttack(angle, self).startAttack();

        if (hasBeenGrounded)
        {
            Vector2 addedForce = new Velocity(10, angle).GetAsVector2();
            self.GetComponent<Rigidbody2D>().velocity += addedForce;
            hasBeenGrounded = false;
        }

        if (way == SlashAttack.Way.Clockwise)
            way = SlashAttack.Way.CounterClockwise;
        else if (way == SlashAttack.Way.CounterClockwise)
            way = SlashAttack.Way.Clockwise;
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        Vector3 nextPos;
        self.GetComponent<Rigidbody2D>().velocity = new Velocity(self.GetComponent<Rigidbody2D>().velocity).setAngle(angle).GetAsVector2();

        LaserAttack laser = (LaserAttack)GetSpecial(angle, self);
        laser.startAttack();

        Velocity laserSize = new Velocity(laser.range, angle);
        float offset = Mathf.Min(Mathf.Abs(self.transform.localScale.x), Mathf.Abs(self.transform.localScale.y)) * 0.4f;

        Collider2D collider = self.GetComponent<Collider2D>();
        if(collider is BoxCollider2D)
        {
            BoxCollider2D box = (BoxCollider2D)collider;
            offset = Mathf.Min(box.size.x, box.size.y) * 0.4f;
        }
        else if(collider is CapsuleCollider2D)
        {
            CapsuleCollider2D box = (CapsuleCollider2D)collider;
            offset = Mathf.Min(box.size.x, box.size.y) * 0.4f;
        }
        else if(collider is CircleCollider2D)
        {
            CircleCollider2D circle = (CircleCollider2D)collider;
            offset = circle.radius;
        }

        Vector3 endLaser = laserSize.GetAsVector2();
        endLaser += laser.posStartLaser;

        RaycastHit2D resultHit = Physics2D.CircleCast(laser.posStartLaser, offset, laserSize.GetAsVector2(), laserSize.getSpeed(), LayerMask.GetMask("Terrain")) ;
        Debug.DrawRay(new Vector2(laser.posStartLaser.x, laser.posStartLaser.y) + new Velocity(offset, angle - 180).GetAsVector2(), laserSize.GetAsVector2(), Color.red, 2f);

        if (resultHit.collider == null)
        {
            nextPos = endLaser;
        }
        else
        {
            Velocity endCollision = new Velocity(laserSize).MulToSpeed(resultHit.fraction);
            laser.range = endCollision.getSpeed();

            nextPos = endCollision.GetAsVector2();
            nextPos += laser.posStartLaser;
        }

        self.transform.position = nextPos;

        self.GetComponent<RotationEntity>().FlipEntityRelativeToGravity(angle);
        self.GetComponent<RotationEntity>().RotateEntityRelativeToFlip(angle);


    }

    public override Attack GetAttack(float angle, Entity self)
    {

        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(swing, new DamageInfo(self, baseAtk * dmg, new Velocity(2 * dmg, angle), baseSDelay + baseSpan + baseEDelay));

        swing.transform.position = startPos;
        swing.AddComponent<SlashAttack>();

        swing.GetComponent<SlashAttack>().audioClip = attackAudio;

        swing.GetComponent<SlashAttack>().src = self;
        swing.GetComponent<SlashAttack>().acceleration = 3.0f;
        swing.GetComponent<SlashAttack>().angle = angle;
        swing.GetComponent<SlashAttack>().range = baseRange;
        swing.GetComponent<SlashAttack>().lenght = baseLenght;
        swing.GetComponent<SlashAttack>().startDelay = baseSDelay;
        swing.GetComponent<SlashAttack>().timeSpan = baseSpan;
        swing.GetComponent<SlashAttack>().endDelay = baseEDelay;
        swing.GetComponent<SlashAttack>().followEntity = forceUnFollow;

        swing.GetComponent<SlashAttack>().way = way;

        return swing.GetComponent<SlashAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject laser = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(laser, new DamageInfo(self, baseAtk * dmg * 5, new Velocity(0.5f * dmg, angle), 1)) ;

        laser.transform.position = startPos;
        laser.AddComponent<LaserAttack>();

        laser.GetComponent<LaserAttack>().audioClip = specialAudio;

        laser.GetComponent<LaserAttack>().src = self;
        laser.GetComponent<LaserAttack>().angle = angle;
        laser.GetComponent<LaserAttack>().range = baseRange * 3;
        laser.GetComponent<LaserAttack>().lenght = self.transform.localScale.x;
        laser.GetComponent<LaserAttack>().startDelay = 0;
        laser.GetComponent<LaserAttack>().timeSpan = 0;
        laser.GetComponent<LaserAttack>().endDelay = 0.05f;
        laser.GetComponent<LaserAttack>().followEntity = false;

        return laser.GetComponent<LaserAttack>();
    }

    //PASSIVE EFFECTS
    public override void OnPickup() {
        Game.player.stat.ModifyAcceleration(0.1f * Game.player.stat.BASE_ACCELERATION);
        Game.player.stat.ModifySpecialCooldown(-0.1f * Game.player.stat.GetSpecialCooldown());
    }

	public override DamageInfo OnDealDamage(DamageInfo damage) {
        damage.amount *= 1 + new Velocity(Game.player.self.GetComponent<Rigidbody2D>().velocity).getSpeed() / Game.player.stat.maxSpeed;
        return damage;
	}

    public override void OnGroundCollisionEnter()
    {
        hasBeenGrounded = true;
    }
}
