using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ArmoredCyborgMovement : EnemyMovement
{
    public float jumpHeight = 2f;

    //private ArmoredCyborgAttack armoredCyborgAttack;
    private EntityCollisionStructure entityCollisionStructure;
    private SpriteRenderer sp;

    //private bool isJumping = false;
    //private bool isFacingWall = false;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        enemyAttack = gameObject.GetComponent<ArmoredCyborgAttack>();
        entityCollisionStructure = gameObject.GetComponent<EntityCollisionStructure>();
        if (entityCollisionStructure == null)
        {
            entityCollisionStructure = gameObject.AddComponent<EntityCollisionStructure>();
            entityCollisionStructure.Init();
        }
        sp = gameObject.GetComponent<SpriteRenderer>();


        nextFlag = flags[0];
        rangeFromPlayerMax = 1.5f;
    }

    public override void FixedUpdate()
    {
        //if (entityCollisionStructure.isGrounded) isJumping = false;

        setNextFlag();

        if(entityCollisionStructure.isGrounded) checkAstarObstacles();

        if (isFollowingPlayer) followingPlayerBehaviour();

        if (!isFollowingPlayer) notFollowingPlayerBehaviour();

        animator.SetFloat("speed", rb.velocity.x);
        if (rb.velocity.x < 0.3 && rb.velocity.x > -0.3) animator.speed = 1;
        else animator.speed = Mathf.Abs(rb.velocity.x);

    }

    public override void moveFollowingPlayer()
    {
        if (Vector2.Distance(nextFlag.position, transform.position) >= rangeFromPlayerMax)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(path.vectorPath[currentWayPoint].x - transform.position.x, 0).normalized * movementSpeed, 5 * Time.deltaTime);
            if (Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]) < 1f) currentWayPoint++;
        }

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

        /*if (!(isFacingWall && entityCollisionStructure.isWalled) && Vector2.Distance(nextFlag.position, transform.position) >= rangeFromPlayerMax)
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
        }*/
    }

    protected override void moveNotFollowingPlayer()
    {
        if (!entityCollisionStructure.isWalled) rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(path.vectorPath[currentWayPoint].x - transform.position.x, 0).normalized * movementSpeed * 0.75f, 5 * Time.deltaTime);
    }

    protected override void moveNotFollowingPlayerOneFlag()
    {
        if (!entityCollisionStructure.isWalled) rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(nextFlag.position.x - transform.position.x, 0).normalized * movementSpeed * 0.75f, 5 * Time.deltaTime);
    }

    /*private void checkObstacles()
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
            ((isFollowingPlayer && Vector2.Distance(nextFlag.position, transform.position) >= rangeFromPlayerMax) || ((!isFollowingPlayer) && Mathf.Abs(transform.position.x - nextFlag.position.x) >= Mathf.Abs(transform.localScale.x))))
        {
            float height = Raycasting.checkObstacleJumpable(transform, sp, jumpHeight);
            if (height != 0f)
            {
                isJumping = true;
                jump(height);
            }
        }
    }*/

    private void checkAstarObstacles() //triche
    {
        if (!seeker.IsDone() || path == null) return;
        Vector3 previousWaypoint = path.vectorPath[0];
        foreach (Vector3 waypoint in path.vectorPath)
        {
            if (waypoint.y >= previousWaypoint.y) previousWaypoint = waypoint;
            else break;
        }
        if(previousWaypoint.y > transform.position.y + sp.bounds.size.y / 2)
        {
            float heightToCheck = previousWaypoint.y - (transform.position.y - sp.bounds.size.y / 2);
            float heightToJump = Raycasting.checkHighestObstacle(transform, sp, heightToCheck);
            if (heightToJump > 0 && heightToJump < jumpHeight + 1) jump(heightToJump+0.7f); //triche
            //if (obstacleChecker.GetComponent<ArmoredCyborgCheckObstacles>() != null && obstacleChecker.GetComponent<ArmoredCyborgCheckObstacles>().isColliding && height < jumpHeight + 1) jump(height);
        }
    }

    private void jump(float height)
    {
        print("JUMP");
        Velocity jump =  new Velocity(GetComponent<GravityEntity>().gravity);

        jump.AddToAngle(180);
        jump.setSpeed(height*jump.getSpeed());
        rb.velocity = jump.GetAsVector2();
    }
}
