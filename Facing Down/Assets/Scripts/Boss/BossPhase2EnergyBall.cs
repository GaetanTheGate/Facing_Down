using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhase2EnergyBall : StateMachineBehaviour
{
    Vector2 playerPosition;
    EnergyBall energyBall;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPosition = Game.player.self.gameObject.transform.position;
        energyBall = new EnergyBall("Player");
        Transform cannon = animator.transform.Find("Cannon");
        energyBall.SetBaseAtk(50);
        energyBall.startPos = cannon.position;
        energyBall.WeaponSpecial(Angles.AngleBetweenVector2(cannon.position, playerPosition), animator.GetComponent<Entity>());
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
