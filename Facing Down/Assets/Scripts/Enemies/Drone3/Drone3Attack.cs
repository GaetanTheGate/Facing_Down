using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone3Attack : EnemyAttack
{
    private Bullet bullet;

    public override void Start()
    {
        base.Start();
        bullet = new Bullet("Player");
    }
    public override void attackPlayer(Vector2 playerPosition)
    {
        if (timePassed >= delay && !isAttacking && canAttack)
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
        fire(targetPosition);
    }

    protected void fire(Vector2 targetPosition)
    {
        bullet.Attack(Angles.AngleBetweenVector2(gameObject.transform.position, targetPosition), gameObject.GetComponent<Entity>());
        isAttacking = false;
    }
}
