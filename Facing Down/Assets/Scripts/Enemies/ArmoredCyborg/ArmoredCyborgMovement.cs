using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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

    //ASTAR
    private Path path;
    private Seeker seeker;
    private int currentWayPoint = 0;
    //ASTAR

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


        nextFlag = flags[0];
        seeker = gameObject.GetComponent<Seeker>();
        InvokeRepeating("updatePath", 0f, 0.3f);
    }

    void onPathComputed(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void updatePath()
    {
        if (seeker.IsDone()) seeker.StartPath(rb.position, nextFlag.position, onPathComputed);
    }

    private void FixedUpdate()
    {
        if (entityCollisionStructure.isGrounded) isJumping = false;

        setNextFlag();

        //checkObstacles();
        if(entityCollisionStructure.isGrounded) checkAstarObstacles();

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
        if (path == null) return;
        if (currentWayPoint >= path.vectorPath.Count) return;
        if (Vector2.Distance(nextFlag.position, transform.position) >= rangeFromPlayerMax)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(path.vectorPath[currentWayPoint].x - transform.position.x, 0).normalized * movementSpeed, 5 * Time.deltaTime);
            if (Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]) < 1f) currentWayPoint++;
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

    private void notFollowingPlayerBehaviour()
    {
        if (path == null) return;
        if (currentWayPoint >= path.vectorPath.Count) return;
        if(!entityCollisionStructure.isWalled) rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(path.vectorPath[currentWayPoint].x - transform.position.x, 0).normalized * movementSpeed, 5 * Time.deltaTime);
        if (Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]) < 1f) currentWayPoint++;
        if (Mathf.Abs(transform.position.x - nextFlag.position.x) < Mathf.Abs(transform.localScale.x))
        {
            tempNext = (tempNext + 1) % flags.Length;
        }

        if ((!isFlipped && rb.velocity.x < 0) || (isFlipped && rb.velocity.x > 0))
        {
            isFlipped = !isFlipped;
            animator.SetBool("isFlipped", isFlipped);
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }
        nextFlag = flags[tempNext];


        /*nextFlag = flags[tempNext];
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
        }*/
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
            ((isFollowingPlayer && Vector2.Distance(nextFlag.position, transform.position) >= rangeFromPlayerMax) || ((!isFollowingPlayer) && Mathf.Abs(transform.position.x - nextFlag.position.x) >= Mathf.Abs(transform.localScale.x))))
        {
            float height = Raycasting.checkObstacleJumpable(transform, sp, jumpHeight);
            if (height != 0f)
            {
                isJumping = true;
                jump(height);
            }
        }
    }

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
            print(heightToCheck + " " +heightToJump);
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
