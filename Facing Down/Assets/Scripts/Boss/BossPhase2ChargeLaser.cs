using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2ChargeLaser : StateMachineBehaviour
{
    Vector3 originalScale;
    float scaleToChange;
    float timePassedAnim;
    float timePassedTotal;
    float delayAnim;
    float delayTotal;
    int count;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        originalScale = animator.transform.localScale;
        scaleToChange = originalScale.x / 80;
        timePassedAnim = 0f;
        timePassedTotal = 0f;
        delayAnim = 0.05f;
        delayTotal = 1.5f;
        count = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timePassedAnim += Time.deltaTime;
        timePassedTotal += Time.deltaTime;

        if (timePassedTotal >= delayTotal)
        {
            animator.SetBool("isChargingLaser", false);
            return;
        }
        else if (timePassedAnim >= delayAnim)
        {
            count += 1;
            Vector3 tempScale = new Vector3();
            tempScale.x = Mathf.Abs(animator.transform.localScale.x) + scaleToChange;
            tempScale.y = Mathf.Abs(animator.transform.localScale.y) - scaleToChange;
            tempScale.x = animator.transform.localScale.x < 0 ? -tempScale.x : tempScale.x;
            tempScale.y = animator.transform.localScale.y < 0 ? -tempScale.y : tempScale.y;
            animator.transform.localScale = tempScale;
            if (count == 10) scaleToChange = -scaleToChange;
            else if (count == 20)
            {
                scaleToChange = -scaleToChange;
                count = 0;
            }
            timePassedAnim = 0f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.localScale = originalScale;
        animator.SetBool("isChargingLaser", false);
        Destroy(animator.GetComponent<BossChargeLaserParticles>().pSystem.gameObject);
    }
}
