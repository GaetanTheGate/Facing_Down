using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class EnemyMovement : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float movementSpeed = 5f;
    public Transform[] flags;
    protected Transform nextFlag;
    protected int tempNext = 0;
    protected Animator animator;
    protected Transform playerTransform;
    protected GameObject player;
    public float aggroDistance = 2f;
    public float aggroViewDistance = 10f;
    public float angleViewDistance = 135f;
    public float rangeFromPlayerMin = 4f;
    public float rangeFromPlayerMax = 5f;
    protected bool isFollowingPlayer = false;

    protected bool isFlipped = false;

    protected EnemyAttack enemyAttack;

    protected bool isWaiting = false;

    //ASTAR
    protected Path path;
    protected Seeker seeker;
    protected int currentWayPoint = 0;
    //ASTAR

    public virtual void Start()
    {
        rb = GetComponent<Entity>().GetRB();
        player = Game.player.self.gameObject;
        playerTransform = player.transform;
        animator = gameObject.GetComponent<Animator>();

        seeker = gameObject.GetComponent<Seeker>();
        InvokeRepeating("updatePath", 0f, 0.2f);
    }

    public virtual void FixedUpdate()
    {
        setNextFlag();

        if (isFollowingPlayer) followingPlayerBehaviour();

        if (!isFollowingPlayer) notFollowingPlayerBehaviour();

        animator.SetFloat("speed", rb.velocity.x);
    }

    protected void onPathComputed(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    protected void updatePath()
    {
        if (seeker.IsDone()) seeker.StartPath(rb.position, nextFlag.position, onPathComputed);
    }

    protected void setNextFlag()
    {
        if (checkRayCastsHitTag(Raycasting.castRayFanInAngleFromEntity(transform, isFlipped ? 180 : 0, angleViewDistance, aggroViewDistance), "Player") ||
                Vector2.Distance(transform.position, playerTransform.position) <= aggroDistance)
        {
            nextFlag = playerTransform;
            isFollowingPlayer = true;
        }
    }

    protected virtual void followingPlayerBehaviour()
    {
        if (path == null) return;
        if (currentWayPoint >= path.vectorPath.Count) return;

        moveFollowingPlayer();

        enemyAttack.attackPlayer(nextFlag.position);
    }

    public abstract void moveFollowingPlayer();

    protected void notFollowingPlayerBehaviour()
    {
        if (isWaiting) return;
        if (path == null) return;
        if (currentWayPoint >= path.vectorPath.Count) return;
        if (flags.Length > 1) moveNotFollowingPlayer();
        else 
        {
            if (Vector2.Distance(transform.position, nextFlag.position) < 0.2f) rb.velocity = new Vector2(0, 0);
            else moveNotFollowingPlayerOneFlag();
        }
        if (Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]) < 1f) currentWayPoint++;
        if (calculateDistanceNextFlag() < Mathf.Abs(transform.localScale.x))
        {
            StartCoroutine(waitIdle(2f));
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

    protected abstract float calculateDistanceNextFlag();

    protected virtual void moveNotFollowingPlayer()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, (path.vectorPath[currentWayPoint] - transform.position).normalized * movementSpeed, 5 * Time.deltaTime);
    }

    protected virtual void moveNotFollowingPlayerOneFlag()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, (nextFlag.position - transform.position).normalized * movementSpeed, 5 * Time.deltaTime);
    }

    protected IEnumerator waitIdle(float duration = 1f)
    {
        isWaiting = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(duration);
        isWaiting = false;
    }

    protected bool checkRayCastsHitTag(List<RaycastHit2D> hits, string tag)
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
