using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAttack : Attack
{
    public Way way;
    public override Vector3 Behaviour(float percentage)
    {
        float percentageTime = percentage - 0.5f;

        float radius = Mathf.Max(src.transform.localScale.x, src.transform.localScale.y, src.transform.localScale.z) / 2 + range / 2.0f;
        transform.localScale = new Vector3(range, transform.localScale.y, transform.localScale.z);

        float relativeAngle;
        if (way == Way.CounterClockwise)
            relativeAngle = lenght * percentageTime;
        else
            relativeAngle = lenght * -percentageTime;

        relativeAngle += angle;

        transform.rotation = Quaternion.Euler(0, 0, relativeAngle);

        Vector3 relativePos = new Vector3();
        relativePos.x = radius * Mathf.Cos(relativeAngle * Mathf.Deg2Rad);
        relativePos.y = radius * Mathf.Sin(relativeAngle * Mathf.Deg2Rad);
        relativePos.z = 0;

        return relativePos;
    }

    public enum Way
    {
        CounterClockwise, Clockwise
    }

}
