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

    public List<Vector2> contactNormalsRelativeToGravity = new List<Vector2>();

    public override void Init()
    {
        gravityEntity = gameObject.GetComponent<GravityEntity>();
        if (gravityEntity == null)
        {
            gravityEntity = gameObject.AddComponent<GravityEntity>();
            gravityEntity.Init();
        }
    }

    private void FixedUpdate()
    {
        if (!isGrounded) isEnteringGround = false;
        if (!isWalled) isEnteringWall = false;
        if (!isCeilinged) isEnteringCeiling = false;
     }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Terrain")))
        {
            contactNormalsRelativeToGravity = new List<Vector2>();
            foreach (ContactPoint2D contact in collision.contacts)
            {
                float angle = Vector2.Angle(gravityEntity.gravity.GetAsVector2(), contact.normal);
                //angle = Mathf.Round(angle);
                if (angle <= 180.0f && angle >= 135.0f)
                {
                    if (isGrounded) continue;
                    isEnteringGround = true;
                    isGroundFirstFrame = false;
                }

                else if (angle < 135.0f && angle >= 45.0f)
                {
                    if (isWalled) continue;
                    isEnteringWall = true;
                    isWallFirstFrame = false;
                }

                else if (angle <= 45.0f && angle >= 0.0f)
                {
                    if (isCeilinged) continue;
                    isEnteringCeiling = true;
                    isCeilingFirstFrame = false;
                }
                contactNormalsRelativeToGravity.Add(new Velocity(contact.normal).SubToAngle(gravityEntity.gravity.getAngle() - 270).GetAsVector2());
            }
            //if (isEnteringGround || isEnteringCeiling || isEnteringWall)
                //Game.player.inventory.OnGroundCollisionEnter();
        }

        /*if (collision.collider.CompareTag("Traps"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (Vector2.Angle(gravityEntity.gravity.GetAsVector2(), contact.normal) <= 180.0f && Vector2.Angle(gravityEntity.gravity.GetAsVector2(), contact.normal) >= 135.0f)
                {
                    isEnteringGround = true;
                    isWallFirstFrame = false;
                }
            }
        }*/
    }

    void OnCollisionStay2D(Collision2D col)
    {
        checkCollisionOnStay(col);
    }

    public virtual void checkCollisionOnStay(Collision2D col)
    {

        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Terrain")))
        {
            contactNormalsRelativeToGravity = new List<Vector2>();
            foreach (ContactPoint2D contact in col.contacts)
            {
                float angle = Vector2.Angle(gravityEntity.gravity.GetAsVector2(), contact.normal);
                //angle = Mathf.Round(angle);
                if (angle <= 180.0f && angle >= 135.0f)
                {
                    isGrounded = true;
                    if (isGroundFirstFrame) isEnteringGround = true;
                    else isEnteringGround = false;
                    //if (isGrounded == false) statPlayer.numberOfDashes = 0;
                }

                else if (angle < 135.0f && angle >= 45.0f)
                {
                    isWalled = true;
                    if (isWallFirstFrame) isEnteringWall = true;
                    else isEnteringWall = false;
                }

                else if (angle <= 45.0f && angle >= 0.0f)
                {
                    isCeilinged = true;
                    if (isCeilingFirstFrame) isEnteringCeiling = true;
                    else isEnteringCeiling = false;
                }

                contactNormalsRelativeToGravity.Add(new Velocity(contact.normal).SubToAngle(gravityEntity.gravity.getAngle() - 270).GetAsVector2());
            }

            isGroundFirstFrame = !this.isGrounded;
            isWallFirstFrame = !this.isWalled;
            isCeilingFirstFrame = !this.isCeilinged;
            //if (isEnteringGround || isEnteringCeiling || isEnteringWall)
            //Game.player.inventory.OnGroundCollisionEnter();
            //print("ground, wall, ceiling : " + isEnteringGround + " " + isEnteringWall + " " + isEnteringCeiling);
        }

        /*if (col.collider.CompareTag("Traps"))
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
        }*/
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer.Equals(LayerMask.NameToLayer("Terrain")))
        {
            isGrounded = false;
            isWalled = false;
            isCeilinged = false;
            //isEnteringGround = false;
            //isEnteringWall = false;
            //isEnteringCeiling = false;
            //isGroundFirstFrame = true;
            //isWallFirstFrame = true;
            //isCeilingFirstFrame = true;
        }
        /*if (col.collider.CompareTag("Traps"))
        {
            isGrounded = false;
        }*/

    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
