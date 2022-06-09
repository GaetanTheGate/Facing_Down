using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MeleeWeapon
{
    public Gun() : this("Enemy") { }

    private float maxSpread = 45f;
    private float spread = 0f;

    public Gun(string target) : base(target, "Gun")
    {
        attackWeapon = new Bullet(target);
        specialWeapon = new Laser(target);

        attackWeapon.SetBaseAtk(40);
        specialWeapon.SetBaseAtk(150);
        specialWeapon.SetBaseSDelay(0);
        specialWeapon.SetBaseSpan(0.2f);
        specialWeapon.SetBaseEDelay(2 - specialWeapon.getSpan());

        baseSpan = 0.1f;
        baseCooldown = 0.0f;

        isAuto = true;

        stat.maxDashes = 6;
        stat.maxSpecial = 6;

        stat.HPMult = 0.75f;
        stat.specialDurationMult = 1.25f;
        stat.specialCooldownMult = 1.25f;

        attackPath = "Prefabs/Items/Weapons/Gun";
        specialPath = "Prefabs/Items/Weapons/Gun";
    }

    private Weapon attackWeapon;
    private Weapon specialWeapon;

    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).startAttack();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        GetSpecial(angle, self).startAttack();
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject gun = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);
        gun.AddComponent<GunAttack>();
        gun.GetComponent<GunAttack>().transform.position = startPos;

        gun.GetComponent<GunAttack>().src = self;
        gun.GetComponent<GunAttack>().startDelay = baseSDelay;
        gun.GetComponent<GunAttack>().timeSpan = baseSpan;
        gun.GetComponent<GunAttack>().endDelay = baseEDelay;
        gun.GetComponent<GunAttack>().lenght = 1;
        gun.GetComponent<GunAttack>().followEntity = forceUnFollow;

        float randomAngle = angle;
        randomAngle += Random.Range(-spread, spread);

        gun.GetComponent<GunAttack>().angle = randomAngle;
        gun.GetComponent<GunAttack>().attack = attackWeapon;

        return gun.GetComponent<GunAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject gun = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);
        gun.AddComponent<GunAttack>();
        gun.GetComponent<GunAttack>().transform.position = startPos;

        gun.GetComponent<GunAttack>().src = self;
        gun.GetComponent<GunAttack>().startDelay = baseSDelay + 1.0f;
        gun.GetComponent<GunAttack>().timeSpan = specialWeapon.getSDelay() + specialWeapon.getSpan() + specialWeapon.getEDelay();
        gun.GetComponent<GunAttack>().endDelay = baseEDelay + 0.5f;
        gun.GetComponent<GunAttack>().lenght = 1;
        gun.GetComponent<GunAttack>().followEntity = false;

        gun.GetComponent<GunAttack>().angle = angle;
        gun.GetComponent<GunAttack>().attack = specialWeapon;
        gun.GetComponent<GunAttack>().isSpecial = true;

        return gun.GetComponent<GunAttack>();
    }

    public override void _Move(float angle, Entity self)
    {
        canMove = false;

        self.GetComponent<Rigidbody2D>().velocity = self.GetComponent<Rigidbody2D>().velocity * 0.2f;

        float gravitySpeed = self.GetComponent<GravityEntity>().gravity.getSpeed();
        self.GetComponent<GravityEntity>().gravity.setSpeed(0.00001f);

        Game.coroutineStarter.StartCoroutine(StartLooseGravity(1f, 10, self, gravitySpeed));
    }

    private IEnumerator StartLooseGravity(float delay, float duration, Entity self, float gravitySpeed)
    {
        yield return new WaitForSeconds(delay);

        Rigidbody2D rb = self.GetComponent<Rigidbody2D>();
        rb.velocity = new Velocity(self.GetComponent<GravityEntity>().gravity).SubToAngle(180).setSpeed(20).GetAsVector2();

        Game.coroutineStarter.LaunchCoroutine(SetVelocityToZeroLoop(self));
        Game.coroutineStarter.LaunchCoroutine(RestoreGravity(duration, self, gravitySpeed));
    }

    private IEnumerator RestoreGravity(float duration, Entity self, float speed)
    {
        yield return new WaitForSeconds(duration);
        self.GetComponent<GravityEntity>().gravity.setSpeed(speed);

        canMove = true;
    }

    private IEnumerator SetVelocityToZeroLoop(Entity self)
    {
        yield return new WaitForFixedUpdate();

        self.GetComponent<Rigidbody2D>().velocity = Vector3.Lerp(self.GetComponent<Rigidbody2D>().velocity, new Vector3(), 2 * Time.fixedDeltaTime);

        if ( ! canMove )
            Game.coroutineStarter.LaunchCoroutine(SetVelocityToZeroLoop(self));
    }

    //PASSIVE EFFECTS

    private float lastAttackTime = 0;
    public override void OnPickup() {
        Game.player.stat.ModifyMaxSpecial(1);
        Game.player.stat.ModifySpecialDuration(Game.player.stat.BASE_SPE_DURATION * 0.10f);
    }

	public override void BeforeAttack() {
        if (lastAttackTime + 2 < Time.time) spread = 0;
        spread = Mathf.Min(maxSpread, spread + 0.4f);
        lastAttackTime = Time.time;
    }

    private IEnumerator startBuffDecay() {
        yield return new WaitForSeconds(5);
        spread = Mathf.Min(maxSpread, spread + 0.2f);
    }
}
