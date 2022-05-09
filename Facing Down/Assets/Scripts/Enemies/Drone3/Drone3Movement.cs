using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Drone3Movement : MonoBehaviour
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
    private Transform nearestFlag;

    private Drone3Attack drone3Attack;

    private bool isFlipped = false;

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
        drone3Attack = gameObject.GetComponent<Drone3Attack>();

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
        setNextFlag();

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
            rb.velocity = Vector2.Lerp(rb.velocity, (path.vectorPath[currentWayPoint] - transform.position).normalized * movementSpeed, 5 * Time.deltaTime);
            if (Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]) < 1f) currentWayPoint++;
        }
        else if (Vector2.Distance(nextFlag.position, transform.position) < rangeFromPlayerMin)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (transform.position - nextFlag.position).normalized * movementSpeed, 5 * Time.deltaTime);
        }
        else if (Vector2.Distance(nextFlag.position, transform.position) >= rangeFromPlayerMin && Vector2.Distance(nextFlag.position, transform.position) < rangeFromPlayerMax)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0, 0), 5 * Time.deltaTime);
        }

        drone3Attack.attackPlayer(nextFlag.position);

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
        if (path == null) return;
        if (currentWayPoint >= path.vectorPath.Count) return;
        rb.velocity = Vector2.Lerp(rb.velocity, (path.vectorPath[currentWayPoint] - transform.position).normalized * movementSpeed, 5 * Time.deltaTime);
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

