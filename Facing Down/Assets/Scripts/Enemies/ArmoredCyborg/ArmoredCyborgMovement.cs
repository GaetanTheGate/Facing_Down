using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ArmoredCyborgMovement : EnemyMovement
{

    //private ArmoredCyborgAttack armoredCyborgAttack;
    private EntityCollisionStructure entityCollisionStructure;

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


        nextFlag = flags[0];
        rangeFromPlayerMax = 1.5f;
    }

    public override void FixedUpdate()
    {
        setNextFlag();

        if (isFollowingPlayer) followingPlayerBehaviour();

        if (!isFollowingPlayer) notFollowingPlayerBehaviour();

        animator.SetFloat("speed", rb.velocity.x);
        if (rb.velocity.x < 0.3 && rb.velocity.x > -0.3) animator.speed = 1;
        else animator.speed = Mathf.Abs(rb.velocity.x);

    }

    protected override void followingPlayerBehaviour()
    {
        base.followingPlayerBehaviour();
        turnShield();

    }

    public override void moveFollowingPlayer()
    {
        if (Vector2.Distance(nextFlag.position, transform.position) >= rangeFromPlayerMax)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(path.vectorPath[currentWayPoint].x - transform.position.x, 0).normalized * movementSpeed, 5 * Time.deltaTime);
            if (Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]) < 1f) currentWayPoint++;
        }
        if (gameObject.transform.Find("ShieldPivot_y").Find("ShieldPivot_z").transform.localRotation.eulerAngles.z > 90 && !isFlipped)
        {
            isFlipped = true;
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }
        else if (gameObject.transform.Find("ShieldPivot_y").Find("ShieldPivot_z").transform.localRotation.eulerAngles.z < 90 && isFlipped)
        {
            isFlipped = false;
            gameObject.transform.localScale = new Vector2(-gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        }
    }

    protected override void moveNotFollowingPlayer()
    {
        if (!entityCollisionStructure.isWalled) rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(path.vectorPath[currentWayPoint].x - transform.position.x, 0).normalized * movementSpeed * 0.75f, 5 * Time.deltaTime);
    }

    protected override void moveNotFollowingPlayerOneFlag()
    {
        if (!entityCollisionStructure.isWalled) rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(nextFlag.position.x - transform.position.x, 0).normalized * movementSpeed * 0.75f, 5 * Time.deltaTime);
    }

    protected override float calculateDistanceNextFlag()
    {
        return Mathf.Abs(transform.position.x - nextFlag.position.x);
    }

    private void turnShield()
    {
        float angleMax = 45 * Time.fixedDeltaTime;

        float anglePlayer = Angles.AngleBetweenVector2(transform.position, playerTransform.position);
        float angleShield = gameObject.transform.Find("ShieldPivot_y").Find("ShieldPivot_z").transform.localRotation.eulerAngles.z;


        anglePlayer = Utility.mod(anglePlayer, 360);

        if (anglePlayer > 180 && anglePlayer <= 270)
        {
            anglePlayer = 180;
            angleShield = anglePlayer;
        }
        else if (anglePlayer > 270 && anglePlayer <= 360)
        {
            anglePlayer = 0;
            angleShield = anglePlayer;
        }


        float newAngle = Mathf.MoveTowardsAngle(angleShield, anglePlayer, angleMax);
        gameObject.transform.Find("ShieldPivot_y").Find("ShieldPivot_z").transform.localRotation = Quaternion.Euler(0,0, newAngle);


        if (isFlipped)
            gameObject.transform.Find("ShieldPivot_y").localRotation = Quaternion.Euler(0, 180, 0);
        else
            gameObject.transform.Find("ShieldPivot_y").localRotation = Quaternion.Euler(0, 0, 0);
    }
}
