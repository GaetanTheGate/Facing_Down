using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ArmoredCyborgMovement : EnemyMovement
{

    //private ArmoredCyborgAttack armoredCyborgAttack;
    private EntityCollisionStructure entityCollisionStructure;
    private bool isAtBorder;

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
        isAtBorder = false;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

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
        
        if (!isAtBorder)
        {
            bool shouldMove = true;
            if (path.vectorPath.Count > currentWayPoint + 1)
            {
                if (!((!isFlipped && (new Vector2(path.vectorPath[currentWayPoint + 1].x - transform.position.x, 0).normalized * movementSpeed).x > 0) || (isFlipped && (new Vector2(path.vectorPath[currentWayPoint + 1].x - transform.position.x, 0).normalized * movementSpeed).x < 0)))
                {
                    shouldMove = false;
                }
            }
            if (shouldMove && Vector2.Distance(nextFlag.position, transform.position) >= rangeFromPlayerMax)
            {
                rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(path.vectorPath[currentWayPoint].x - transform.position.x, 0).normalized * movementSpeed, 5 * Time.deltaTime);
                if (Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]) < 1f) currentWayPoint++;
            }
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
            if( !(angleShield < 180 && angleShield > 0))
                angleShield = anglePlayer;
        }
        else if (anglePlayer > 270 && anglePlayer <= 360)
        {
            anglePlayer = 0;
            if (!(angleShield < 180 && angleShield > 0))
                angleShield = anglePlayer;
        }


        float newAngle = Mathf.MoveTowardsAngle(angleShield, anglePlayer, angleMax);
        gameObject.transform.Find("ShieldPivot_y").Find("ShieldPivot_z").transform.localRotation = Quaternion.Euler(0,0, newAngle);


        if (isFlipped)
            gameObject.transform.Find("ShieldPivot_y").localRotation = Quaternion.Euler(0, 180, 0);
        else
            gameObject.transform.Find("ShieldPivot_y").localRotation = Quaternion.Euler(0, 0, 0);

        Transform shieldTransform = gameObject.transform.Find("ShieldPivot_y").Find("ShieldPivot_z").Find("Shield");

        shieldTransform.localScale = new Vector3(shieldTransform.localScale.x, Mathf.Abs(shieldTransform.localScale.y) * gameObject.transform.Find("ShieldPivot_y").Find("ShieldPivot_z").localRotation.z > 0.5f ? -1 : 1, shieldTransform.localScale.z);
    }

    public void childTriggerExitGround()
    {
        isAtBorder = true;
    }

    public void childTriggerEnterGround()
    {
        isAtBorder = false;
    }
}
