using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wings : MeleeWeapon
{
    private float difference = 20.0f;

    public Wings() : this("Enemy") { }
    public Wings(string target) : base(target, "Wings")
    {

        baseAtk = 50;
        baseRange = 2.5f;
        baseLenght = 180;
        baseSpan = 0.3f;
        baseEDelay = 0.1f;
        baseCooldown = - baseEDelay - baseSpan / 2;

        attackPath = "Prefabs/Weapons/Wings";
        specialPath = "Prefabs/Weapons/Wings";
    }


    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).startAttack();

        Game.coroutineStarter.LaunchCoroutine(onEndAttack(baseSpan, self, angle));
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo dmgInfo = new DamageInfo(self, baseAtk * dmg, new Velocity(2 * dmg, angle));
        AddHitAttack(swing, dmgInfo);

        swing.transform.position = startPos;
        swing.AddComponent<HalfSlashAttack>();

        swing.GetComponent<HalfSlashAttack>().src = self;
        swing.GetComponent<HalfSlashAttack>().endDelay = baseEDelay;
        swing.GetComponent<HalfSlashAttack>().range = baseRange;
        swing.GetComponent<HalfSlashAttack>().lenght = baseLenght - difference;
        swing.GetComponent<HalfSlashAttack>().timeSpan = baseSpan;
        swing.GetComponent<HalfSlashAttack>().followEntity = forceUnFollow;
        swing.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;

        GameObject swing2 = GameObject.Instantiate(swing);
        swing2.GetComponent<AttackHit>().dmgInfo = dmgInfo;


        swing.GetComponent<HalfSlashAttack>().angle = angle - difference;
        swing.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.Clockwise;

        swing2.GetComponent<HalfSlashAttack>().angle = angle + difference;
        swing2.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.CounterClockwise;

        GameObject attack = new GameObject();
        attack.AddComponent<CompositeAttack>();
        attack.GetComponent<CompositeAttack>().attackList.Add(swing.GetComponent<HalfSlashAttack>());
        attack.GetComponent<CompositeAttack>().attackList.Add(swing2.GetComponent<HalfSlashAttack>());

        return attack.GetComponent<CompositeAttack>();
    }

    public override Attack GetSpecial(float angle, Entity self)
    {
        GameObject swing = GameObject.Instantiate(Resources.Load(specialPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        DamageInfo dmgInfo = new DamageInfo(self, baseAtk * dmg * 5, new Velocity(4 * dmg, angle));
        AddHitAttack(swing, dmgInfo);

        swing.transform.position = startPos;
        swing.AddComponent<HalfSlashAttack>();

        swing.GetComponent<HalfSlashAttack>().src = self;
        swing.GetComponent<HalfSlashAttack>().endDelay = baseEDelay * 2;
        swing.GetComponent<HalfSlashAttack>().range = baseRange * 2;
        swing.GetComponent<HalfSlashAttack>().lenght = baseLenght - difference;
        swing.GetComponent<HalfSlashAttack>().timeSpan = baseSpan * 2;
        swing.GetComponent<HalfSlashAttack>().followEntity = forceUnFollow;
        swing.GetComponent<HalfSlashAttack>().inOut = HalfSlashAttack.InOut.In;

        GameObject swing2 = GameObject.Instantiate(swing);
        swing2.GetComponent<AttackHit>().dmgInfo = dmgInfo;


        swing.GetComponent<HalfSlashAttack>().angle = angle - difference;
        swing.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.Clockwise;

        swing2.GetComponent<HalfSlashAttack>().angle = angle + difference;
        swing2.GetComponent<HalfSlashAttack>().way = HalfSlashAttack.Way.CounterClockwise;


        GameObject attack = new GameObject();
        attack.AddComponent<CompositeAttack>();
        attack.GetComponent<CompositeAttack>().attackList.Add(swing.GetComponent<HalfSlashAttack>());
        attack.GetComponent<CompositeAttack>().attackList.Add(swing2.GetComponent<HalfSlashAttack>());

        return attack.GetComponent<CompositeAttack>();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        canAttack = false;
        canSpecial = false;

        GetSpecial(angle, self).startAttack();

        Game.coroutineStarter.LaunchCoroutine(onEndSpecial(baseSpan * 2, self, angle));
    }


    private IEnumerator onEndAttack(float delay, Entity self, float angle)
    {
        yield return new WaitForSeconds(delay);

        Velocity newVelo = new Velocity(baseAtk / 6, angle + 180 + difference);
        self.GetComponent<Rigidbody2D>().velocity += newVelo.GetAsVector2();
    }

    private IEnumerator onEndSpecial(float delay, Entity self, float angle)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log(angle);
        Velocity newVelo = new Velocity(baseAtk / 3, angle + 180 + difference);
        self.GetComponent<Rigidbody2D>().velocity += newVelo.GetAsVector2();

        canAttack = true;
        canSpecial = true;
    }

    public override void _Move(float angle, Entity self)
    {
        canMove = false;

        self.GetComponent<Rigidbody2D>().velocity = self.GetComponent<Rigidbody2D>().velocity * 0.2f;

        float gravitySpeed = self.GetComponent<GravityEntity>().gravity.getSpeed();
        self.GetComponent<GravityEntity>().gravity.setSpeed(0.00001f);

        Game.coroutineStarter.StartCoroutine(StartLooseGravity(0.5f, 10, self, gravitySpeed));
    }

    private IEnumerator StartLooseGravity(float delay, float duration, Entity self, float gravitySpeed)
    {
        yield return new WaitForSeconds(delay);

        Rigidbody2D rb = self.GetComponent<Rigidbody2D>();
        rb.velocity += new Velocity(self.GetComponent<GravityEntity>().gravity).SubToAngle(180).setSpeed(1).GetAsVector2();

        Game.coroutineStarter.StartCoroutine(RestoreGravity(duration, self, gravitySpeed));
    }

    private IEnumerator RestoreGravity(float duration, Entity self, float speed)
    {
        yield return new WaitForSeconds(duration);
        self.GetComponent<GravityEntity>().gravity.setSpeed(speed);

        canMove = true;
    }
}
