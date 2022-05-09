using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Drone3Movement : EnemyMovement
{

    //private Drone3Attack drone3Attack;

    

    // Start is called before the first frame update
    public override void Start()
    {

        base.Start();

        enemyAttack = gameObject.GetComponent<Drone3Attack>();
        nextFlag = flags[0];
    }

    public override void moveFollowingPlayer()
    {
        if (Vector2.Distance(nextFlag.position, transform.position) >= rangeFromPlayerMax)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (path.vectorPath[currentWayPoint] - transform.position).normalized * movementSpeed, 5 * Time.deltaTime);
            if (Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]) < 1f) currentWayPoint++;
        }
        else if (Vector2.Distance(nextFlag.position, transform.position) < rangeFromPlayerMin)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (transform.position - nextFlag.position).normalized * movementSpeed, 5 * Time.deltaTime);
        }
        /*else if (Vector2.Distance(nextFlag.position, transform.position) >= rangeFromPlayerMin && Vector2.Distance(nextFlag.position, transform.position) < rangeFromPlayerMax)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0, 0), 5 * Time.deltaTime);
        }*/

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

}

