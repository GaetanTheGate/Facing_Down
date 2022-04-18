using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionStructure : AbstractPlayer
{
    private Entity self;
    private StatEntityPlayer statEntityPlayer;
    private bool isGrounded = false;
    private bool isWalled = false;
    private bool isCeilinged = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Init()
    {
        self = gameObject.GetComponent<Player>().self;
        statEntityPlayer = self.GetComponent<StatEntityPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
                if (Vector2.Angle(Vector2.down, contact.normal) <= 180.0f && Vector2.Angle(Vector2.down, contact.normal) >= 135.0f)
                {
                    groundedTest = true;
                    if (isGrounded == false) statEntityPlayer.numberOfDashes = 0;
                }

                else if (Vector2.Angle(Vector2.down, contact.normal) < 135.0f && Vector2.Angle(Vector2.down, contact.normal) >= 45.0f)
                {
                    walledTest = true;
                }

                else if (Vector2.Angle(Vector2.down, contact.normal) <= 45.0f && Vector2.Angle(Vector2.down, contact.normal) >= 0.0f)
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
                if (Vector2.Angle(Vector2.down, contact.normal) <= 180.0f && Vector2.Angle(Vector2.down, contact.normal) >= 135.0f)
                {
                    groundedTest = true;
                    if (isGrounded == false) statEntityPlayer.numberOfDashes = 0;
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
