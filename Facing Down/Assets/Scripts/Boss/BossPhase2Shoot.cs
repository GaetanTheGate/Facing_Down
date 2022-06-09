using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2Shoot : StateMachineBehaviour
{
    Vector2 playerPosition;
    Bullet bullet;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPosition = Game.player.self.gameObject.transform.position;
        bullet = new Bullet("Player");
        bullet.SetBaseAtk(30);
        bullet.startPos = animator.transform.Find("Cannon").transform.position;
        bullet.WeaponSpecial(Angles.AngleBetweenVector2(animator.transform.position, playerPosition), animator.GetComponent<Entity>());
        animator.SetTrigger("idle");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
