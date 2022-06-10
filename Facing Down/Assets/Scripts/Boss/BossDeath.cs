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
        Game.coroutineStarter.LaunchCoroutine(slowTime(1f));
        Game.coroutineStarter.LaunchCoroutine(waitDeathDuration(deathAnimationDuration));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (canEndState)
        {
            animator.SetBool("isDying", false);
            ParticleSystem.MainModule mainSparks = animator.GetComponent<BossDeathParticles>().pSystemSparks.main;
            mainSparks.loop = false;
            animator.GetComponent<BossDeathParticles>().pSystemExplosion.Stop();
            animator.SetBool("isDyingFinalExplosion", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isDying", false);
        animator.SetBool("isDyingFinalExplosion", false);
    }

    private IEnumerator waitDeathDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        canEndState = true;
    }

    private IEnumerator slowTime(float duration)
    {
        float timePassed = 0f;
        while(timePassed < duration)
        {
            Game.time.SetGameSpeedInstant(0.2f);
            yield return new WaitForEndOfFrame();
            timePassed += Time.deltaTime;
        }
    }
}
