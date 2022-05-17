using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : StateMachineBehaviour
{
    float timePassed;
    float delay;
    int random;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timePassed = 0f;
        delay = Random.Range(0.5f, 2f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timePassed += Time.deltaTime;
        if (timePassed >= delay)
        {
            random = Random.Range(1, 6);
            if (random >=1 && random <= 4)
            {
                animator.SetTrigger("shoot");
            }
            else if (random == 5)
            {
                animator.SetBool("isChargingLaser", true);
            }
        }
        

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("phase2");
        //animator.ResetTrigger("shoot");
    }
}
