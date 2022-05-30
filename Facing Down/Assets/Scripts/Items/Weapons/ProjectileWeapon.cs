using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileWeapon : Weapon
{
    protected float baseSpeed;

    public void SetBaseSpeed(float speed) => baseSpeed = speed;

    public ProjectileWeapon(string target, string id) : base(target, id)
    {

    }

    protected override void AddHitAttack(GameObject gameObject, DamageInfo dmgInfo)
    {
        base.AddHitAttack(gameObject, dmgInfo);
        gameObject.GetComponent<AttackHit>().dmgInfo.isMelee = false;
    }
}
