using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAttack : MeleeAttack
{
    private bool hasShot = false;

    public bool forceUnfollow = false;
    public Weapon attack;
    public bool isSpecial = false;
    public override Vector3 Behaviour(float percentage)
    {
        if( ! hasShot && percentage > 0)
        {
            hasShot = true;

            attack.forceUnFollow = forceUnfollow;
            attack.startPos = transform.GetChild(0).position;
            if (isSpecial)
                attack.WeaponSpecial(angle, src);
            else
                attack.WeaponAttack(angle, src);
        }

        float radius = Mathf.Max(src.transform.localScale.x, src.transform.localScale.y, src.transform.localScale.z) / 2 + transform.localScale.x / 2;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        float relativeScaleY;
        if (angle > 90 && angle < 270)
            relativeScaleY = -1;
        else
            relativeScaleY = 1;
        transform.localScale = new Vector3(lenght, relativeScaleY * lenght, transform.localScale.z);

        Vector3 relativePos = new Vector3();
        relativePos.x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        relativePos.y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        relativePos.z = 0;

        return relativePos;
    }

    private void computeNewAttack(Attack attack)
    {

        if (attack.GetType().IsSubclassOf(typeof(MeleeAttack)))
        {
            MeleeAttack newAttack = (MeleeAttack)attack;
            newAttack.followEntity = false;

        }
        else if (attack.GetType().IsSubclassOf(typeof(ThrowableAttack)))
        {
        }
        else if (attack.GetType().IsSubclassOf(typeof(CompositeAttack)))
        {
            CompositeAttack newAttack = (CompositeAttack)attack;
            foreach (Attack subAttack in newAttack.attackList)
                computeNewAttack(subAttack);
        }
    }

    private Vector2 RotatePoint(Vector2 c, float angle, Vector2 p)
    {
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);

        // translate point back to origin:
        Vector2 newP = p-c;

        // rotate point
        float xnew = p.x * cos - p.y * sin;
        float ynew = p.x * sin + p.y * cos;

        // translate point back:
        newP.x = xnew + c.x;
        newP.y = ynew + c.y;
        return newP;
    }
}
