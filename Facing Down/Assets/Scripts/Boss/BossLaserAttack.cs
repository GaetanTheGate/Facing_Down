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

    private void laserAttackPhase2(float angleOffset, float duration)
    {
        laser.SetBaseAtk(200);
        laser.SetBaseSDelay(0);
        laser.SetBaseSpan(0.3f);
        laser.SetBaseLenght(2);
        laser.SetBaseEDelay(Mathf.Max(0, duration - 0.3f));
        List<Attack> attackList = new List<Attack>
        {
            laser.GetSpecial(45 + angleOffset, entity),
            //laser.GetSpecial(90 + angleOffset, entity),
            laser.GetSpecial(135 + angleOffset, entity),
            laser.GetSpecial(225 + angleOffset, entity),
            //laser.GetSpecial(270 + angleOffset, entity),
            laser.GetSpecial(315 + angleOffset, entity)
        };
        foreach (Attack attack in attackList)
        {
            attack.startAttack();
        }
        StartCoroutine(moveLasers(attackList, 0.05f));
    }

    private void FixedUpdate()
    {
        if (!wasDoneOnce && animator.GetBool("isLaserAttackActive"))
        {
            wasDoneOnce = true;
            if (animator.GetBool("isPhase2")) laserAttackPhase2(animator.GetFloat("laserAngleOffset"), animator.GetFloat("laserAttackDuration"));
            else laserAttack(animator.GetFloat("laserAngleOffset"), animator.GetFloat("laserAttackDuration"));
        }
        if (!animator.GetBool("isLaserAttackActive"))
        {
            wasDoneOnce = false;
        }
    }

    private IEnumerator moveLasers(List<Attack> attackList, float repeatRate)
    {
        yield return new WaitForSeconds(0.1f);
        while(animator.GetBool("isLaserAttackActive"))
        {
            foreach (Attack attack in attackList)
            {
                attack.angle += 1;
            }
            yield return new WaitForSeconds(repeatRate);
        }
    }
}
