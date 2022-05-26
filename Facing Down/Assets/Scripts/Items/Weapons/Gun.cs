using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MeleeWeapon
{
    public Gun() : this("Enemy") { }
    public Gun(string target) : base(target, "Gun")
    {
        attackWeapon = new Bullet(target);
        specialWeapon = new Laser(target);

        baseSpan = 0.1f;
        baseCooldown = 0.0f;

        isAuto = true;

        stat.HPMult = 0.75f;
        stat.specialDurationMult = 1.25f;
        stat.specialCooldownMult = 1.25f;

        attackPath = "Prefabs/Weapons/Gun";
        specialPath = "Prefabs/Weapons/Gun";
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
        randomAngle += Random.Range(-5.0f, 5.0f);

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
}
