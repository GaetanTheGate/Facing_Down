using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone3Attack : MonoBehaviour
{
    public float delay = 2f;
    private float timePassed = 0f;
    private bool isAttacking = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timePassed += Time.fixedDeltaTime;
    }

    public void attackPlayer(Vector2 playerPosition)
    {
        if (timePassed >= delay && !isAttacking)
        {
            isAttacking = true;
            if (Vector2.Angle(playerPosition, gameObject.transform.position) > 10f)
            {
                if (playerPosition.y > gameObject.transform.position.y)
                {
                    animator.SetTrigger("attackForwardUp");
                    StartCoroutine(startAttackAnimationRoutine(animator.GetCurrentAnimatorStateInfo(0).length, playerPosition));
                }
                else
                {
                    animator.SetTrigger("attackForwardDown");
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

    private IEnumerator startAttackAnimationRoutine(float duration, Vector2 targetPosition)
    {
        yield return new WaitForSeconds(duration);
        fire(targetPosition);
    }

    private void fire(Vector2 targetPosition)
    {
        Debug.Log("fire");
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Ammo/Bullet", typeof(GameObject)) as GameObject);
        bullet.transform.position = gameObject.transform.position;
        //bullet.GetComponent<BulletMovement>().moveToPosition(targetPosition);
        isAttacking = false;
    }
}
