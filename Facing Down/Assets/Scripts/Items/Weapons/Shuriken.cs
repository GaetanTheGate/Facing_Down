using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : ProjectileWeapon
{
    public Shuriken() : this("Enemy") { }
    public Shuriken(string target) : base(target, "Shuriken")
    {
        baseAtk = 50f;
        baseSpeed = 25.0f;
        baseSpan = 0.2f;
        baseEDelay = 5.0f;
        baseCooldown = -baseEDelay + (baseSpan / 2);

        attackPath = "Prefabs/Weapons/Shuriken";
        specialPath = "Prefabs/Weapons/BouncyShuriken";
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject shuriken = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(shuriken, new DamageInfo(self, baseAtk * dmg, new Velocity(1.0f * dmg, angle)));

        shuriken.AddComponent<ProjectileAttack>();
        shuriken.transform.position = startPos;

        shuriken.GetComponent<ProjectileAttack>().src = self;

        shuriken.GetComponent<ProjectileAttack>().angle = angle;
        shuriken.GetComponent<ProjectileAttack>().acceleration = 1.0f;
        shuriken.GetComponent<ProjectileAttack>().timeSpan = baseSpan;
        shuriken.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        shuriken.GetComponent<ProjectileAttack>().speed = baseSpeed;
        shuriken.GetComponent<ProjectileAttack>().rotationSpeed = shuriken.GetComponent<ProjectileAttack>().speed * (angle > 90 && angle <= 270 ? -36 : 36);
        shuriken.GetComponent<ProjectileAttack>().gravity = self.GetComponent<GravityEntity>().gravity;


        return shuriken.GetComponent<ProjectileAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject shuriken = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo dmgInfo = new DamageInfo(self, baseAtk * dmg * 2, new Velocity(1.0f * dmg, angle));
        AddHitAttack(shuriken, dmgInfo);

        shuriken.AddComponent<ProjectileAttack>();
        shuriken.transform.position = startPos;

        shuriken.GetComponent<ProjectileAttack>().src = self;

        shuriken.GetComponent<ProjectileAttack>().angle = angle;
        shuriken.GetComponent<ProjectileAttack>().acceleration = 1.0f;
        shuriken.GetComponent<ProjectileAttack>().timeSpan = baseSpan;
        shuriken.GetComponent<ProjectileAttack>().endDelay = 10;
        shuriken.GetComponent<ProjectileAttack>().speed = baseSpeed * 0.75f;
        shuriken.GetComponent<ProjectileAttack>().rotationSpeed = shuriken.GetComponent<ProjectileAttack>().speed * (angle > 90 && angle <= 270 ? -36 : 36);
        shuriken.GetComponent<ProjectileAttack>().gravity = self.GetComponent<GravityEntity>().gravity;
        shuriken.GetComponent<ProjectileAttack>().numberOfHitToDestroy = 10;

        GameObject shuriken2 = GameObject.Instantiate(shuriken);
        shuriken2.GetComponent<ProjectileAttack>().angle -= Random.Range(10, 45);
        Physics2D.IgnoreCollision(shuriken.GetComponent<Collider2D>(), shuriken2.GetComponent<Collider2D>());
        shuriken2.GetComponent<AttackHit>().dmgInfo = dmgInfo;
        shuriken2.GetComponent<ProjectileAttack>().gravity = self.GetComponent<GravityEntity>().gravity;

        GameObject shuriken3 = GameObject.Instantiate(shuriken);
        shuriken3.GetComponent<ProjectileAttack>().angle += Random.Range(10, 45);
        Physics2D.IgnoreCollision(shuriken.GetComponent<Collider2D>(), shuriken3.GetComponent<Collider2D>());
        shuriken3.GetComponent<AttackHit>().dmgInfo = dmgInfo;
        shuriken3.GetComponent<ProjectileAttack>().gravity = self.GetComponent<GravityEntity>().gravity;

        Physics2D.IgnoreCollision(shuriken2.GetComponent<Collider2D>(), shuriken3.GetComponent<Collider2D>());

        GameObject attack = new GameObject();
        attack.AddComponent<CompositeAttack>();
        attack.GetComponent<CompositeAttack>().attackList.Add(shuriken.GetComponent<ProjectileAttack>());
        attack.GetComponent<CompositeAttack>().attackList.Add(shuriken2.GetComponent<ProjectileAttack>());
        attack.GetComponent<CompositeAttack>().attackList.Add(shuriken3.GetComponent<ProjectileAttack>());

        return attack.GetComponent<CompositeAttack>();
    }

    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).startAttack();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        GetSpecial(angle, self).startAttack();
    }

    public override void _Move(float angle, Entity self)
    {
        canMove = false;

        PhysicsMaterial2D bouciness = new PhysicsMaterial2D
        {
            bounciness = 1.1f
        };

        self.GetComponent<Collider2D>().sharedMaterial = bouciness;

        self.GetComponent<Rigidbody2D>().velocity = new Velocity(20, angle).GetAsVector2();
        self.GetComponent<Rigidbody2D>().freezeRotation = false;

        Game.coroutineStarter.LaunchCoroutine(EndBounce(10f, self));
    }

    private IEnumerator EndBounce(float delay, Entity self)
    {
        yield return new WaitForSeconds(delay);

        self.GetComponent<Collider2D>().sharedMaterial = null;

        canMove = true;

        yield return new WaitUntil(new System.Func<bool>(self.GetComponent<EntityCollisionStructure>().IsGrounded));
        Game.coroutineStarter.LaunchCoroutine(EndFreezeRotation(1f, self));
    }

    private IEnumerator EndFreezeRotation(float delay, Entity self)
    {
        yield return new WaitForSeconds(delay);

        self.GetComponent<Rigidbody2D>().freezeRotation = true;
    }
}
