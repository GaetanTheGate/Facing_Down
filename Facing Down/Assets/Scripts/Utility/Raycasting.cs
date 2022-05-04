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

    public static bool checkObstacleJumpable(Transform objTransform, SpriteRenderer objSpriteRenderer, float jumpHeight)
    {
        float xPos;
        for (int i = 0; i <= jumpHeight/0.1f + 1; i++)
        {
            xPos = objTransform.localScale.x < 0 ? objTransform.position.x - objSpriteRenderer.bounds.size.x / 2 + Mathf.Epsilon : objTransform.position.x + objSpriteRenderer.bounds.size.x / 2 + Mathf.Epsilon;
            Debug.DrawRay(new Vector2(xPos, objTransform.position.y - objSpriteRenderer.bounds.size.y / 2 - Mathf.Epsilon + i*0.1f), new Vector2(objTransform.localScale.x, 0), Color.green);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(xPos, objTransform.position.y - objSpriteRenderer.bounds.size.y / 2 - Mathf.Epsilon + i * 0.1f), new Vector2(objTransform.localScale.x, 0), 0.1f);
            if (hit.collider == null) return true;
        }
        return false;
    }
}
