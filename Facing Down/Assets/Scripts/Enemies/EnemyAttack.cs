using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    protected float delay = 2f;
    protected float timePassed = 0f;
    protected bool isAttacking = false;
    protected bool canAttack = true;

    

    protected Animator animator;

    // Start is called before the first frame update
    public virtual void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public virtual void FixedUpdate()
    {
        passTime();
    }

    protected void passTime()
    {
        timePassed += Time.fixedDeltaTime;
    }

    public abstract void attackPlayer(Vector2 playerPosition);

    public void disableAttack()
    {
        canAttack = false;
    }
}
