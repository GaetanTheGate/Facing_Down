using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2Idle : StateMachineBehaviour
{
    float timePassed;
    float delay;
    int random;
    bool isChoiceDone;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timePassed = 0f;
        delay = Random.Range(0.5f, 1.5f);
        isChoiceDone = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timePassed += Time.deltaTime;
        if (!isChoiceDone && timePassed >= delay)
        {
            isChoiceDone = true;
            random = Random.Range(1, 11);
            if (random >= 0 && random <= 4)
            {
                animator.SetTrigger("shoot");
            }
            else if (random >= 5 && random <= 8)
            {
                animator.SetTrigger("energyBall");
            }
            else if (random >= 9 && random <= 10)
            {
                animator.SetFloat("laserAngleOffset", Random.Range(0, 2) * 45);
                animator.SetBool("isChargingLaser", true);
            }
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("shoot");
    }
}
