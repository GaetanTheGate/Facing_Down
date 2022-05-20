using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : StateMachineBehaviour
{
    //Vector2 playerPosition;
    float laserAttackDuration;
    bool canEndState;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //playerPosition = Game.player.self.gameObject.transform.position;
        //laser.Attack(Angles.AngleBetweenVector2(animator.transform.position, playerPosition), entity);
        laserAttackDuration = 1f;
        canEndState = false;
        animator.SetFloat("laserAttackDuration", laserAttackDuration);
        animator.SetBool("isLaserAttackActive", true);
        Game.coroutineStarter.LaunchCoroutine(waitLaserDuration(laserAttackDuration));
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(canEndState) animator.SetBool("isLaserAttackActive", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isLaserAttackActive", false);
        List<Attack> tmp = animator.GetComponent<BossLaserAttack>().laserAttacks;
        Debug.Log(tmp.Count);
        for (int i = 0; i < tmp.Count; i++)
        {
            if(tmp[i] != null) Destroy(tmp[i].gameObject);
        }
    }

    private IEnumerator waitLaserDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        canEndState = true;
    }
}
