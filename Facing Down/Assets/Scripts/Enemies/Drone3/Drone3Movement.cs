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
    public float aggroDistance = 5;
    private bool isFollowingPlayer = false;
    private bool wasFollowingPlayer = false;
    private Transform nearestFlag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = Game.player.self.gameObject;
        playerTransform = player.transform;
        animator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        nearestFlag = flags[0];
        foreach (Transform flag in flags)
        {
            if (Vector2.Distance(transform.position, flag.position) < Vector2.Distance(transform.position, nearestFlag.position)) nearestFlag = flag;
        } 
        if (Vector2.Distance(nearestFlag.position, playerTransform.position) <= aggroDistance )
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

        if (isFollowingPlayer && Vector2.Distance(nextFlag.position, transform.position) >= player.GetComponent<CapsuleCollider2D>().size.magnitude/2) rb.velocity = new Vector2(nextFlag.position.x - transform.position.x, nextFlag.position.y - transform.position.y).normalized * movementSpeed * Time.deltaTime;
        else if (isFollowingPlayer && Vector2.Distance(nextFlag.position, transform.position) < player.GetComponent<CapsuleCollider2D>().size.magnitude/2) rb.velocity = new Vector2(0, 0);
        else if (!isFollowingPlayer) rb.velocity = new Vector2(nextFlag.position.x - transform.position.x, nextFlag.position.y - transform.position.y).normalized * movementSpeed * Time.deltaTime;
        if((!isFollowingPlayer) && (Vector2.Distance(transform.position, nextFlag.position) < 0.1f))
        {
            tempNext = (tempNext + 1) % flags.Length;
            wasFollowingPlayer = false;
        }
        animator.SetFloat("speed", rb.velocity.x);
    }

    public void disableMovement()
    {
        enabled = false;
    }

}
