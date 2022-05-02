using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAttack : ThrowableAttack
{
    public bool forceUnfollow = false;
    public Weapon weapon;
    public bool isSpecial = false;

    protected override void attackEnd()
    {
        weapon.forceUnFollow = forceUnfollow;
        weapon.startPos = transform.position;
        if (isSpecial)
            weapon.WeaponSpecial(angle, src);
        else
            weapon.WeaponAttack(angle, src);
    }
}
