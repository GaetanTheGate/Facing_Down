using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCollisionStructure : AbstractEntity
{
    private GravityEntity gravityEntity;
    [HideInInspector]
    public bool isGrounded = false;
    [HideInInspector]
    public bool isWalled = false;
    [HideInInspector]
    public bool isCeilinged = false;

    public override void Init()
    {
        gravityEntity = gameObject.GetComponent<GravityEntity>();
    }

    void OnCollisionStay2D(Collision2D col)
    {
        bool groundedTest = false;
        bool walledTest = false;
        bool ceilingedTest = false;

        if (col.collider.CompareTag("Terrain"))
        {
            groundedTest = false;
            walledTest = false;
            ceilingedTest = false;
            foreach (ContactPoint2D contact in col.contacts)
            {
                float angle = Vector2.Angle(gravityEntity.gravity.GetAsVector2(), contact.normal);
                if (angle <= 180.0f && angle >= 135.0f)
                {
                    groundedTest = true;
                    //if (isGrounded == false) statPlayer.numberOfDashes = 0;
                }

                else if (angle < 135.0f && angle >= 45.0f)
                {
                    walledTest = true;
                }

                else if (angle <= 45.0f && angle >= 0.0f)
                {
                    ceilingedTest = true;
                }
            }

            isGrounded = groundedTest;
            isWalled = walledTest;
            isCeilinged = ceilingedTest;
        }

        if (col.collider.CompareTag("Traps"))
        {
            groundedTest = false;
            foreach (ContactPoint2D contact in col.contacts)
            {
                if (Vector2.Angle(gravityEntity.gravity.GetAsVector2(), contact.normal) <= 180.0f && Vector2.Angle(gravityEntity.gravity.GetAsVector2(), contact.normal) >= 135.0f)
                {
                    groundedTest = true;
                    //if (isGrounded == false) statPlayer.numberOfDashes = 0;
                }
            }
            isGrounded = groundedTest;
        }

    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Terrain"))
        {
            isGrounded = false;
            isWalled = false;
            isCeilinged = false;
        }
        if (col.collider.CompareTag("Traps"))
        {
            isGrounded = false;
        }

    }
}
