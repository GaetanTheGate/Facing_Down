using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0.0f, 1000.0f)] public float movementSpeed = 200;
    public float jumpVelocity = 350;
    public int maxJumps = 1;

    private Rigidbody2D rb;
    private bool isJumping = false;
    private int numberOfJumps = 0;
    private float movement = 0.0f;
    private bool isGrounded = false;
    private bool isWalled = false;
    private bool isCeilinged = false;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && numberOfJumps < maxJumps)
        {
            numberOfJumps += 1;          
            isJumping = true;
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log("ADDFORCE : " + movement * movementSpeed * Time.deltaTime);
        //rb.AddForce(new Vector2(movement * movementSpeed * Time.deltaTime, 0));
        rb.velocity = new Vector2(movement * movementSpeed * Time.deltaTime, rb.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        if(rb.velocity.x > 0.0f)
        {
            spriteRenderer.flipX = false;
        }else if(rb.velocity.x < 0.0f)
        {
            spriteRenderer.flipX = true;
        }

        if (isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * jumpVelocity * Time.deltaTime);
            isJumping = false;
        }
        if (rb.velocity.y < 0)
        {
            rb.velocity += new Vector2(0, Physics2D.gravity.y * 1.5f * Time.deltaTime);
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += new Vector2(0, Physics2D.gravity.y * 1.2f * Time.deltaTime);
        }
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
                    if(isGrounded==false) numberOfJumps = 0;
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
                    if (isGrounded == false) numberOfJumps = 0;
                }
            }
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

    }
}
