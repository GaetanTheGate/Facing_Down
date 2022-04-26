using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MeleeAttack
{
    public override Vector3 Behaviour(float percentage)
    {
        float relativePercentage = percentage * 2;
        relativePercentage = relativePercentage > 1 ? 1 : relativePercentage;
        transform.localScale = new Vector3(range, lenght * relativePercentage, transform.localScale.z);

        float radius = range / 2;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 relativePos = new Vector3();
        relativePos.x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        relativePos.y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        relativePos.z = 0;

        return relativePos;
    }
}
