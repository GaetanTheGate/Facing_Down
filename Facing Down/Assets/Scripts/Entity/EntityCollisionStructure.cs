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
    [HideInInspector]
    public bool isEnteringGround = false;
    [HideInInspector]
    public bool isEnteringWall = false;
    [HideInInspector]
    public bool isEnteringCeiling = false;
    private bool isGroundFirstFrame = true;
    private bool isWallFirstFrame = true;
    private bool isCeilingFirstFrame = true;

    public override void Init()
    {
        gravityEntity = gameObject.GetComponent<GravityEntity>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Terrain"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                float angle = Vector2.Angle(gravityEntity.gravity.GetAsVector2(), contact.normal);
                if (angle <= 180.0f && angle >= 135.0f)
                {
                    isEnteringGround = true;
                    isGroundFirstFrame = false;
                }

                else if (angle < 135.0f && angle >= 45.0f)
                {
                    isEnteringWall = true;
                    isWallFirstFrame = false;
                }

                else if (angle <= 45.0f && angle >= 0.0f)
                {
                    isEnteringCeiling = true;
                    isCeilingFirstFrame = false;
                }
            }
            //if (isEnteringGround || isEnteringCeiling || isEnteringWall)
                //Game.player.inventory.OnGroundCollisionEnter();
        }

        if (collision.collider.CompareTag("Traps"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (Vector2.Angle(gravityEntity.gravity.GetAsVector2(), contact.normal) <= 180.0f && Vector2.Angle(gravityEntity.gravity.GetAsVector2(), contact.normal) >= 135.0f)
                {
                    isEnteringGround = true;
                    isWallFirstFrame = false;
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        bool groundedTest = false;
        bool walledTest = false;
        bool ceilingedTest = false;
        bool isEnteringGroundTest = false;
        bool isEnteringWallTest = false;
        bool isEnteringCeilingTest = false;

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
                    if (isGroundFirstFrame) isEnteringGroundTest = true;
                    //if (isGrounded == false) statPlayer.numberOfDashes = 0;
                }

                else if (angle < 135.0f && angle >= 45.0f)
                {
                    walledTest = true;
                    if (isWallFirstFrame) isEnteringWallTest = true;
                }

                else if (angle <= 45.0f && angle >= 0.0f)
                {
                    ceilingedTest = true;
                    if (isCeilingFirstFrame) isEnteringCeilingTest = true;
                }
            }

            isGrounded = groundedTest;
            isWalled = walledTest;
            isCeilinged = ceilingedTest;
            isEnteringGround = isEnteringGroundTest;
            isEnteringWall = isEnteringWallTest;
            isEnteringCeiling = isEnteringCeilingTest;

            isGroundFirstFrame = !isGrounded;
            isWallFirstFrame = !isWalled;
            isCeilingFirstFrame = !isCeilinged;
            if (isEnteringWall) print("WOW!");
            //if (isEnteringGround || isEnteringCeiling || isEnteringWall)
                //Game.player.inventory.OnGroundCollisionEnter();
            //print("ground, wall, ceiling : " + isEnteringGround + " " + isEnteringWall + " " + isEnteringCeiling);
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
            isEnteringGround = false;
            isEnteringWall = false;
            isEnteringCeiling = false;
            isGroundFirstFrame = true;
            isWallFirstFrame = true;
            isCeilingFirstFrame = true;
        }
        if (col.collider.CompareTag("Traps"))
        {
            isGrounded = false;
        }

    }
}
