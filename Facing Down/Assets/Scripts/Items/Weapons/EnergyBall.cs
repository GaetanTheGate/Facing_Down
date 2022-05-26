using UnityEngine;

public class EnergyBall : ProjectileWeapon
{
    //private float angleRange = 20.0f;

    public EnergyBall() : this("Enemy") { }
    public EnergyBall(string target) : base(target, "EnergyBall")
    {

        baseAtk = 30f;
        baseSpeed = 5;
        baseSpan = 3f;
        baseEDelay = 7.0f;
        baseCooldown = -baseEDelay + 0.1f;

        attackPath = "Prefabs/Weapons/EnergyBall";
        specialPath = "Prefabs/Weapons/EnergyBall";
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject energyBall = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(energyBall, new DamageInfo(self, baseAtk * dmg, new Velocity(0.125f * dmg, angle), baseSDelay + baseSpan + baseEDelay));

        energyBall.AddComponent<ProjectileAttack>();
        energyBall.transform.position = startPos;

        energyBall.GetComponent<ProjectileAttack>().src = self;
        energyBall.GetComponent<ProjectileAttack>().layersToDestroyOn.Add(target);

        energyBall.GetComponent<ProjectileAttack>().angle = angle;
        energyBall.GetComponent<ProjectileAttack>().acceleration = 0.2f;
        energyBall.GetComponent<ProjectileAttack>().startDelay = baseSDelay;
        energyBall.GetComponent<ProjectileAttack>().timeSpan = baseSpan;
        energyBall.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        energyBall.GetComponent<ProjectileAttack>().speed = baseSpeed;

        return energyBall.GetComponent<ProjectileAttack>();
    }

    //private int numberOfShot = 20;

    public float rangeMax = 20f;

    public override Attack GetSpecial(float angle, Entity self)
    {
        Transform following = null;

        /*for (float range = 1f; range < 20; range += 1){

        }*/
        Collider2D collider = Physics2D.OverlapCircle(self.transform.position, rangeMax, LayerMask.GetMask(target));
        if (collider != null)
            following = collider.transform;
        Debug.Log(following.gameObject.name);
        GameObject energyBall = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(energyBall, new DamageInfo(self, baseAtk * dmg, new Velocity(0.125f * dmg, angle), baseSDelay + baseSpan + baseEDelay));

        energyBall.AddComponent<ProjectileAttack>();
        energyBall.transform.position = startPos;

        energyBall.GetComponent<ProjectileAttack>().src = self;
        energyBall.GetComponent<ProjectileAttack>().layersToDestroyOn.Add(target);

        energyBall.GetComponent<ProjectileAttack>().angle = angle;
        energyBall.GetComponent<ProjectileAttack>().acceleration = 0.8f;
        energyBall.GetComponent<ProjectileAttack>().startDelay = baseSDelay;
        energyBall.GetComponent<ProjectileAttack>().timeSpan = baseSpan;
        energyBall.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        energyBall.GetComponent<ProjectileAttack>().speed = baseSpeed;
        energyBall.GetComponent<ProjectileAttack>().followTransform = following;
        energyBall.GetComponent<ProjectileAttack>().followMaxAngle = 180f;

        return energyBall.GetComponent<ProjectileAttack>();
    }

    public override void WeaponAttack(float angle, Entity self)
    {
        GetAttack(angle, self).startAttack();
    }

    public override void WeaponSpecial(float angle, Entity self)
    {
        GetSpecial(angle, self).startAttack();
    }
}