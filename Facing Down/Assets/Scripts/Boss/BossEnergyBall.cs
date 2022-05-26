using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnergyBall : StateMachineBehaviour
{
    Vector2 playerPosition;
    EnergyBall energyBall;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPosition = Game.player.self.gameObject.transform.position;
        energyBall = new EnergyBall("Player");
        energyBall.startPos = animator.transform.Find("Cannon").transform.position;
        energyBall.WeaponAttack(Angles.AngleBetweenVector2(animator.transform.position, playerPosition), animator.GetComponent<Entity>());
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
