using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCyborgAttack : EnemyAttack
{
    public float bashRange = 3f;

    /*public override void attackPlayer(Vector2 playerPosition)
    {
        if (timePassed >= delay && !isAttacking)
        {
            isAttacking = true;
            if (Vector2.Angle(playerPosition, gameObject.transform.position) > 10f)
            {
                if (playerPosition.y > gameObject.transform.position.y)
                {
                    animator.SetTrigger("attackUp");
                    StartCoroutine(startAttackAnimationRoutine(animator.GetCurrentAnimatorStateInfo(0).length, playerPosition));
                }
                else
                {
                    animator.SetTrigger("attackDown");
                    StartCoroutine(startAttackAnimationRoutine(animator.GetCurrentAnimatorStateInfo(0).length, playerPosition));
                }
            }
            else
            {
                animator.SetTrigger("attackForward");
                StartCoroutine(startAttackAnimationRoutine(animator.GetCurrentAnimatorStateInfo(0).length, playerPosition));
            }
            timePassed = 0f;
        }
    }

    protected IEnumerator startAttackAnimationRoutine(float duration, Vector2 targetPosition)
    {
        yield return new WaitForSeconds(duration);
        if(canAttack) fire(targetPosition);
    }

    protected void fire(Vector2 targetPosition)
    {
        bullet.Attack(Angles.AngleBetweenVector2(gameObject.transform.position, targetPosition), gameObject.GetComponent<Entity>());
        isAttacking = false;
    }*/

    public override void attackPlayer(Vector2 playerPosition)
    {
        if (timePassed >= delay && !isAttacking && Vector2.Distance(transform.position, playerPosition) <= bashRange)
        {
            isAttacking = true;
            if(canAttack) bash(playerPosition);
        }
    }

    private void bash(Vector2 targetPosition)
    {
        gameObject.transform.Find("ShieldPivot_y").gameObject.SetActive(false);
        //shield.Attack()
        StartCoroutine(bashWait(1f));
    }

    protected IEnumerator bashWait(float vulnerableDuration)
    {
        yield return new WaitForSeconds(vulnerableDuration);
        gameObject.transform.Find("ShieldPivot_y").gameObject.SetActive(true);
        timePassed = 0f;
        isAttacking = false;
    }
}
