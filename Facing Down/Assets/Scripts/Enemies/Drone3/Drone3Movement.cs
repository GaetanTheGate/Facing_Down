using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone3Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float movementSpeed = 100;
    public Transform[] flags;
    private Transform nextFlag;
    private int tempNext = 0;
    private Animator animator;
    private Transform playerTransform;
    private GameObject player;
    public float aggroDistance = 5f;
    public float shootingRangeMin = 2f;
    public float shootingRangeMax = 3f;
    private bool isFollowingPlayer = false;
    private bool wasFollowingPlayer = false;
    private Transform nearestFlag;

    private Drone3Attack drone3Attack;

    private bool isFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = Game.player.self.gameObject;
        playerTransform = player.transform;
        animator = gameObject.GetComponent<Animator>();
        drone3Attack = gameObject.GetComponent<Drone3Attack>();
    }

    private void FixedUpdate()
    {
        //Debug.Log("isFollowing : " + isFollowingPlayer);
        //Debug.Log("Distance : " + Vector2.Distance(transform.position, playerTransform.position));
        //Debug.Log("Angle : " + Vector2.Angle(transform.position, playerTransform.position));
        nearestFlag = flags[0];
        foreach (Transform flag in flags)
        {
            if (Vector2.Distance(transform.position, flag.position) < Vector2.Distance(transform.position, nearestFlag.position)) nearestFlag = flag;
        }
        if (Vector2.Distance(nearestFlag.position, playerTransform.position) <= aggroDistance)
        {

            nextFlag = playerTransform;
            isFollowingPlayer = true;
            wasFollowingPlayer = true;
        }
        else if (wasFollowingPlayer && Vector2.Distance(playerTransform.position, nearestFlag.position) > aggroDistance)
        {
            isFollowingPlayer = false;
            nextFlag = nearestFlag;
        }
        else
        {
            nextFlag = flags[tempNext];
            isFollowingPlayer = false;
        }

        if (isFollowingPlayer && Vector2.Distance(nextFlag.position, transform.position) >= shootingRangeMax)
        {
            rb.velocity = new Vector2(nextFlag.position.x - transform.position.x, nextFlag.position.y - transform.position.y).normalized * movementSpeed * Time.deltaTime;
        }
        else if (isFollowingPlayer && Vector2.Distance(nextFlag.position, transform.position) < shootingRangeMin)
        {
            rb.velocity = new Vector2(transform.position.x - nextFlag.position.x, transform.position.y - nextFlag.position.y).normalized * movementSpeed * Time.deltaTime;
        }
        else if (isFollowingPlayer && Vector2.Distance(nextFlag.position, transform.position) >= shootingRangeMin && Vector2.Distance(nextFlag.position, transform.position) < shootingRangeMax)
        {
            rb.velocity = new Vector2(0, 0);
            drone3Attack.attackPlayer(nextFlag.position);
        }
        else if (!isFollowingPlayer) rb.velocity = new Vector2(nextFlag.position.x - transform.position.x, nextFlag.position.y - transform.position.y).normalized * movementSpeed * Time.deltaTime;
        if ((!isFollowingPlayer) && (Vector2.Distance(transform.position, nextFlag.position) < 0.1f))
        {
            tempNext = (tempNext + 1) % flags.Length;
            wasFollowingPlayer = false;
        }
        animator.SetFloat("speed", rb.velocity.x);

        if(isFollowingPlayer && Vector2.Distance(nextFlag.position, transform.position) >= shootingRangeMin && Vector2.Distance(nextFlag.position, transform.position) < shootingRangeMax)
        {
            if (nextFlag.position.x < transform.position.x && isFlipped == false)
            {
                isFlipped = true;
                animator.SetBool("isFlipped", isFlipped);
                gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
            }
            else if (nextFlag.position.x >= transform.position.x && isFlipped == true)
            {
                isFlipped = false;
                animator.SetBool("isFlipped", isFlipped);
                gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
            }
        }
        else if( (!isFlipped && rb.velocity.x < 0) || (isFlipped && rb.velocity.x > 0) )
        {
            isFlipped = !isFlipped;
            animator.SetBool("isFlipped", isFlipped);
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }
    }

    public void disableMovement()
    {
        enabled = false;
    }

}

