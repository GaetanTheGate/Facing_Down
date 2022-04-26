using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Raycasting
{
    public static List<RaycastHit2D> castRayFanInAngle(Vector2 position, float fanDirectionInDegree, float angle, float distance)
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        hits.Add(Physics2D.Raycast(position, Angles.GetAngleAsVector2(fanDirectionInDegree - angle / 2), distance));
        for (int i = Mathf.CeilToInt(fanDirectionInDegree - angle / 2); i <= Mathf.FloorToInt(fanDirectionInDegree + angle / 2); i++)
        {
            hits.Add(Physics2D.Raycast(position, Angles.GetAngleAsVector2(i), distance));
        }
        hits.Add(Physics2D.Raycast(position, Angles.GetAngleAsVector2(fanDirectionInDegree + angle / 2), distance));
        return hits;
    }

    public static List<RaycastHit2D> castRayFanInAngleFromEntity(Transform transform, float fanDirectionInDegree, float angle, float distance)
    {
        return castRayFanInAngle(transform.position + transform.localScale, fanDirectionInDegree, angle, distance);
    }
}
