using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectAttack : Attack
{
    public override Vector3 Behaviour(float percentage)
    {
        float relativeRange = range * percentage;
        transform.localScale = new Vector3(relativeRange, transform.localScale.y, transform.localScale.z);

        float radius = lenght / 360 * percentage * 2 + relativeRange / 2;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 relativePos = new Vector3();
        relativePos.x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        relativePos.y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        relativePos.z = 0;

        return relativePos;
    }
}
