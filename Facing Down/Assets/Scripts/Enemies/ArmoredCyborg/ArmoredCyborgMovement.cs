using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCyborgMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementSpeed = 200;
    public Transform[] flags;
    private Transform nextFlag;
    private int tempNext = 0;
    private Animator animator;
    private Transform playerTransform;
    private GameObject player;
    public float aggroDistance = 2f;
    public float aggroViewDistance = 10f;
    public float rangeFromPlayerMin = 4f;
    public float rangeFromPlayerMax = 5f;
    private bool isFollowingPlayer = false;
    public float jumpHeight = 2f;
    private Transform nearestFlag;

    private ArmoredCyborgAttack armoredCyborgAttack;
    private EntityCollisionStructure entityCollisionStructure;
    private SpriteRenderer sp;

    private bool isFlipped = false;
    private bool isJumping = false;
    private bool isFacingWall = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Entity>().GetRB();
        player = Game.player.self.gameObject;
        playerTransform = player.transform;
        animator = gameObject.GetComponent<Animator>();
        armoredCyborgAttack = gameObject.GetComponent<ArmoredCyborgAttack>();
        entityCollisionStructure = gameObject.GetComponent<EntityCollisionStructure>();
        if (entityCollisionStructure == null)
        {
            entityCollisionStructure = gameObject.AddComponent<EntityCollisionStructure>();
            entityCollisionStructure.Init();
        }
        sp = gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (entityCollisionStructure.isGrounded) isJumping = false;

        setNextFlag();

        checkObstacles();

        if (isFollowingPlayer) followingPlayerBehaviour();

        if (!isFollowingPlayer) notFollowingPlayerBehaviour();

        animator.SetFloat("speed", rb.velocity.x);

    }

    private void setNextFlag()
    {
        if (checkRayCastsHitTag(Raycasting.castRayFanInAngleFromEntity(transform, isFlipped ? 180 : 0, 135, aggroViewDistance), "Player") ||
                Vector2.Distance(transform.position, playerTransform.position) <= aggroDistance)
        {
            nextFlag = playerTransform;
            isFollowingPlayer = true;
        }
    }

    private void followingPlayerBehaviour()
    {
        if (!(isFacingWall && entityCollisionStructure.isWalled) && Vector2.Distance(nextFlag.position, transform.position) >= rangeFromPlayerMax)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(nextFlag.position.x - transform.position.x, rb.velocity.y).normalized * movementSpeed, 5 * Time.deltaTime);
        }
        else if (Vector2.Distance(nextFlag.position, transform.position) < rangeFromPlayerMin)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(transform.position.x - nextFlag.position.x, rb.velocity.y).normalized * movementSpeed, 5 * Time.deltaTime);
        }

        armoredCyborgAttack.attackPlayer(nextFlag.position);

        if (nextFlag.position.x < transform.position.x && !isFlipped)
        {
            isFlipped = true;
            animator.SetBool("isFlipped", isFlipped);
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }
        else if (nextFlag.position.x >= transform.position.x && isFlipped)
        {
            isFlipped = false;
            animator.SetBool("isFlipped", isFlipped);
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }
    }

    private void notFollowingPlayerBehaviour()
    {
        nextFlag = flags[tempNext];
        if(!entityCollisionStructure.isWalled) rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(nextFlag.position.x - transform.position.x, rb.velocity.y).normalized * movementSpeed, 5 * Time.deltaTime);
        if (Vector2.Distance(transform.position, nextFlag.position) < Mathf.Abs(transform.localScale.x))
        {
            tempNext = (tempNext + 1) % flags.Length;
        }

        if ((!isFlipped && rb.velocity.x < 0) || (isFlipped && rb.velocity.x > 0))
        {
            isFlipped = !isFlipped;
            animator.SetBool("isFlipped", isFlipped);
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }
    }

    private void checkObstacles()
    {
        isFacingWall = false;
        foreach (Vector2 normal in entityCollisionStructure.contactNormalsRelativeToGravity)
        {
            if (new Velocity(normal).getAngle() > 100 && new Velocity(normal).getAngle() < 260)
            {
                if (transform.localScale.x > 0)
                {
                    isFacingWall = true;
                }
            }
            else if (new Velocity(normal).getAngle() > 280 || new Velocity(normal).getAngle() < 80)
            {
                if(transform.localScale.x < 0)
                {
                    isFacingWall = true;
                }
            }

        }
        if (!isJumping && entityCollisionStructure.isWalled && isFacingWall && 
            ((isFollowingPlayer && Vector2.Distance(nextFlag.position, transform.position) >= rangeFromPlayerMax) || ((!isFollowingPlayer) && (Vector2.Distance(transform.position, nextFlag.position) >= 0.1f))))
        {
            if (Raycasting.checkObstacleJumpable(transform, sp, jumpHeight))
            {
                isJumping = true;
                jump();
            }
        }
    }

    private void jump()
    {
        Velocity jump =  new Velocity(GetComponent<GravityEntity>().gravity);

        jump.AddToAngle(180);
        jump.setSpeed(jumpHeight*jump.getSpeed());
        rb.velocity = jump.GetAsVector2();
    }

    private bool checkRayCastsHitTag(List<RaycastHit2D> hits, string tag)
    {
        bool isHittingTag = false;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag(tag)) isHittingTag = true;
        }
        return isHittingTag;
    }

    public void disableMovement()
    {
        enabled = false;
    }
}
