using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : StateMachineBehaviour
{
    float deathAnimationDuration;
    bool canEndState;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        deathAnimationDuration = 2f;
        canEndState = false;
        animator.SetBool("isDying", true);
        Game.coroutineStarter.LaunchCoroutine(waitLaserDuration(deathAnimationDuration));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (canEndState)
        {
            animator.SetBool("isDying", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isDying", false);
    }

    private IEnumerator waitLaserDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        canEndState = true;
    }
}
