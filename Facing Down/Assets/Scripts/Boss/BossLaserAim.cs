using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserAim : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("laserAttackIndicatorDuration", stateInfo.length);
        animator.SetBool("isLaserAttackIndicatorActive", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isLaserAttackIndicatorActive", false);
        List<Attack> tmp = animator.GetComponent<BossLaserAttackIndicator>().laserIndicators;
        for (int i = 0; i < tmp.Count; i++)
        {
            Destroy(tmp[i].gameObject);
        }
    }
}
