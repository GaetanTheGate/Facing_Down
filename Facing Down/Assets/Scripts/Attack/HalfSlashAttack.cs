using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfSlashAttack : Attack
{
    public Way way;
    public InOut inOut;
    public override Vector3 Behaviour(float percentage)
    {
        float relativeScaleY;
        if (way == Way.Clockwise)
            relativeScaleY = -1;
        else
            relativeScaleY = 1;
        float percentageTime = percentage;
        if(inOut == InOut.Out)
            percentageTime = 1 - percentageTime;


        float relativeRange = range * percentageTime;

        float radius = Mathf.Max(src.transform.localScale.x, src.transform.localScale.y, src.transform.localScale.z) / 2 + relativeRange / 2.0f;
        transform.localScale = new Vector3(relativeRange, relativeScaleY * relativeRange, transform.localScale.z);

        float relativeAngle = lenght * (1 - percentageTime);
        if (way == Way.CounterClockwise)
            relativeAngle *= -1;
        else
            relativeAngle *= 1;

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

    public enum InOut
    {
        In, Out
    }
}
