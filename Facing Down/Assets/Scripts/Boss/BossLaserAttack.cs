using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserAttack : MonoBehaviour
{
    private Animator animator;
    private Laser laser;
    private Entity entity;
    bool wasDoneOnce = false;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        laser = new Laser("Player");
        entity = GetComponent<Entity>();
    }

    private void laserAttack(float angleOffset, float duration)
    {
        laser.SetBaseAtk(200);
        laser.SetBaseSDelay(0);
        laser.SetBaseSpan(0.3f);
        laser.SetBaseLenght(2);
        laser.SetBaseEDelay(duration-0.3f);
        laser.Special(45 + angleOffset, entity);
        laser.Special(135 + angleOffset, entity);
        laser.Special(225 + angleOffset, entity);
        laser.Special(315 + angleOffset, entity);
    }

    private void FixedUpdate()
    {
        if (!wasDoneOnce && animator.GetBool("isLaserAttackActive") == true)
        {
            wasDoneOnce = true;
            laserAttack(animator.GetFloat("laserAngleOffset"), animator.GetFloat("laserAttackDuration"));
        }
        if (animator.GetBool("isLaserAttackActive") == false)
        {
            wasDoneOnce = false;
        }
    }
}
