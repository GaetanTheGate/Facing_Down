using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserAttackIndicator : MonoBehaviour
{
    private Animator animator;
    private LaserIndicator laserIndicator;
    private Entity entity;
    public List<Attack> laserIndicators;
    bool wasDoneOnce = false;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        laserIndicator = new LaserIndicator("Player");
        entity = GetComponent<Entity>();
    }

    private List<Attack> laserAttackIndicator(float angleOffset, float duration)
    {

        laserIndicator.SetBaseAtk(0);
        laserIndicator.SetBaseSDelay(0);
        laserIndicator.SetBaseSpan(0.1f);
        laserIndicator.SetBaseLenght(0.1f);
        laserIndicator.SetBaseEDelay(duration - 0.1f);
        List<Attack> attackList = new List<Attack>
        {
            laserIndicator.GetAttack(45 + angleOffset, entity),
            laserIndicator.GetAttack(135 + angleOffset, entity),
            laserIndicator.GetAttack(225 + angleOffset, entity),
            laserIndicator.GetAttack(315 + angleOffset, entity)
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
