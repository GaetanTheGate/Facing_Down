using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCyborgAttack : EnemyAttack
{
    public float bashRange = 3f;
    private StunShot stunShot;

    public override void Start()
    {
        base.Start();
        stunShot = new StunShot("Player");
    }

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
        if (timePassed >= delay && !isAttacking && canAttack)
        {
            isAttacking = true;
            if (Vector2.Distance(transform.position, playerPosition) <= bashRange) bash(playerPosition);
            else shoot(playerPosition);
        }
    }

    private void bash(Vector2 targetPosition)
    {
        gameObject.transform.Find("ShieldPivot_y").gameObject.SetActive(false);
        //shield.Attack()
        StartCoroutine(bashWait(1f));
    }

    private void shoot(Vector2 targetPosition)
    {
        float angle = Angles.AngleBetweenVector2(transform.position, targetPosition);
        Vector3 bulletPos = new Vector3(0, 0, 0);
        float radius = 1.1f;

        if (angle >= 0 && angle <= 90)
        {
            bulletPos.x = radius * Mathf.Cos((angle) * Mathf.Deg2Rad);
            bulletPos.y = radius * Mathf.Sin((angle) * Mathf.Deg2Rad);
        }
        else if (angle > 90 && angle <= 180)
        {
            bulletPos.x = radius * Mathf.Cos((180 - angle) * Mathf.Deg2Rad) * -1;
            bulletPos.y = radius * Mathf.Sin((180 - angle) * Mathf.Deg2Rad);
        }
        else if (angle < 0 && angle >= -90)
        {
            bulletPos.x = radius * Mathf.Cos((angle * -1) * Mathf.Deg2Rad);
            bulletPos.y = radius * Mathf.Sin((angle * -1) * Mathf.Deg2Rad) * -1;
        }
        else if (angle < -90 && angle >= -180)
        {
            bulletPos.x = radius * Mathf.Cos((180 - angle * -1) * Mathf.Deg2Rad) * -1;
            bulletPos.y = radius * Mathf.Sin((180 - angle * -1) * Mathf.Deg2Rad) * -1;
        }
        stunShot.startPos = new Vector3(transform.position.x + bulletPos.x, transform.position.y + bulletPos.y, transform.position.z);
        Attack attack = stunShot.GetAttack(angle, gameObject.GetComponent<Entity>());
        Physics2D.IgnoreCollision(attack.GetComponent<Collider2D>(), transform.Find("ShieldPivot_y").Find("ShieldPivot_z").Find("Shield").GetComponent<Collider2D>(), true);
        attack.startAttack();
        //stunShot.WeaponAttack(angle, gameObject.GetComponent<Entity>());
        StartCoroutine(shootWait(1f));
    }

    protected IEnumerator bashWait(float vulnerableDuration)
    {
        yield return new WaitForSeconds(vulnerableDuration);
        gameObject.transform.Find("ShieldPivot_y").gameObject.SetActive(true);
        timePassed = 0f;
        isAttacking = false;
    }
    
    protected IEnumerator shootWait(float vulnerableDuration)
    {
        yield return new WaitForSeconds(vulnerableDuration);
        timePassed = 0f;
        isAttacking = false;
    }
}
