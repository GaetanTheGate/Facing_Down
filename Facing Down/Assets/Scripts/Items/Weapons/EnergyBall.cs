using UnityEngine;

public class EnergyBall : ProjectileWeapon
{
    //private float angleRange = 20.0f;
    public float FocusRangeMax = 20f;

    public EnergyBall() : this("Enemy") { }
    public EnergyBall(string target) : base(target, "EnergyBall")
    {

        baseAtk = 30f;
        baseSpeed = 8f;
        baseSpan = 3f;
        baseEDelay = 7.0f;
        baseCooldown = -9;

        attackPath = "Prefabs/Items/Weapons/EnergyBall";
        specialPath = "Prefabs/Items/Weapons/EnergyBall";
        attackAudio = Resources.Load<AudioClip>("Sound_Effects/energy_ball");
        specialAudio = Resources.Load<AudioClip>("Sound_Effects/energy_ball");
    }

    public override Attack GetAttack(float angle, Entity self)
    {
        GameObject energyBall = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(energyBall, new DamageInfo(self, baseAtk * dmg, new Velocity(0.125f * dmg, angle), baseSDelay + baseSpan + baseEDelay));

        energyBall.AddComponent<ProjectileAttack>();
        energyBall.transform.position = startPos;

        energyBall.GetComponent<ProjectileAttack>().audioClip = attackAudio;

        energyBall.GetComponent<ProjectileAttack>().src = self;
        energyBall.GetComponent<ProjectileAttack>().layersToDestroyOn.Add(target);

        energyBall.GetComponent<ProjectileAttack>().angle = angle;
        energyBall.GetComponent<ProjectileAttack>().acceleration = 0f;
        energyBall.GetComponent<ProjectileAttack>().startDelay = baseSDelay;
        energyBall.GetComponent<ProjectileAttack>().timeSpan = baseSpan;
        energyBall.GetComponent<ProjectileAttack>().endDelay = baseEDelay;
        energyBall.GetComponent<ProjectileAttack>().speed = baseSpeed;
        energyBall.GetComponent<ProjectileAttack>().isUsingEndAnimation = true;

        return energyBall.GetComponent<ProjectileAttack>();
    }

    //private int numberOfShot = 20;

    

    public override Attack GetSpecial(float angle, Entity self)
    {
        Transform following = null;

        Collider2D collider;
        for (int i = 1; i <= FocusRangeMax; i++)
        {
            collider = Physics2D.OverlapCircle(self.transform.position, i, LayerMask.GetMask(target));
            if (collider != null)
            {
                following = collider.transform;
                break;
            }
                
        }
        
        GameObject energyBall = GameObject.Instantiate(Resources.Load(attackPath, typeof(GameObject)) as GameObject);

        float dmg = self.GetComponent<StatEntity>().getAtk() / 100;
        AddHitAttack(energyBall, new DamageInfo(self, baseAtk * dmg, new Velocity(0.125f * dmg, angle), baseSDelay + baseSpan + baseEDelay));

        energyBall.AddComponent<ProjectileAttack>();
        energyBall.transform.position = startPos;

        energyBall.GetComponent<ProjectileAttack>().audioClip = specialAudio;

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
        energyBall.GetComponent<ProjectileAttack>().isUsingEndAnimation = true;

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
