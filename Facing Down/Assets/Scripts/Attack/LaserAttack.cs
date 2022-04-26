using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MeleeAttack
{
    public float rangeCollide;
    public Vector3 posStartLaser;
    public override Vector3 Behaviour(float percentage)
    {
        posStartLaser = pos;
        
        Vector3 angleDash = new Velocity(1.0f, angle).GetAsVector2();
        RaycastHit2D resultHit = Physics2D.Raycast(posStartLaser, angleDash, range, LayerMask.GetMask("Terrain"));

        if (resultHit.collider == null)
            rangeCollide = range;
        else
            rangeCollide = resultHit.distance;

        float relativePercentage = percentage * 2;
        relativePercentage = relativePercentage > 1 ? 1 : relativePercentage;
        transform.localScale = new Vector3(rangeCollide, lenght * relativePercentage, transform.localScale.z);

        float radius = rangeCollide / 2;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 relativePos = new Vector3();
        relativePos.x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        relativePos.y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        relativePos.z = 0;

        return relativePos;
    }
}
