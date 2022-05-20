using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserAttackIndicator : MonoBehaviour
{
    private Animator animator;
    private Laser laser;
    private Entity entity;
    public List<Attack> laserIndicators;
    bool wasDoneOnce = false;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        laser = new Laser("Player");
        entity = GetComponent<Entity>();
    }

    private List<Attack> laserAttackIndicator(float angleOffset, float duration)
    {

        laser.SetBaseAtk(0);
        laser.SetBaseSDelay(0);
        laser.SetBaseSpan(0.1f);
        laser.SetBaseLenght(0.1f);
        laser.SetBaseEDelay(duration);
        List<Attack> attackList = new List<Attack>
        {
            laser.GetAttack(45 + angleOffset, entity),
            laser.GetAttack(135 + angleOffset, entity),
            laser.GetAttack(225 + angleOffset, entity),
            laser.GetAttack(315 + angleOffset, entity)
        };
        foreach (Attack attack in attackList)
        {
            attack.color = new Color(0, 255, 0, 0.5f);
            attack.startAttack();
        }
        return attackList;
    }

    private void FixedUpdate()
    {
        if (!wasDoneOnce && animator.GetBool("isLaserAttackIndicatorActive") == true)
        {
            wasDoneOnce = true;
            laserIndicators = laserAttackIndicator(animator.GetFloat("laserAngleOffset"), animator.GetFloat("laserAttackIndicatorDuration"));
        }
        if (animator.GetBool("isLaserAttackIndicatorActive") == false)
        {
            wasDoneOnce = false;
        }
    }
}
