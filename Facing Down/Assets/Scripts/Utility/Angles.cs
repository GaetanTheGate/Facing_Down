using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Angles
{
    public static float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    public static Vector2 GetAngleAsVector2(float angle)
    {
        float x = Mathf.Cos(Mathf.Deg2Rad * angle);
        float y = Mathf.Sin(Mathf.Deg2Rad * angle);

        return new Vector2(x, y);
    }
}
