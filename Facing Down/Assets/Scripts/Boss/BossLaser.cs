using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : StateMachineBehaviour
{
    //Vector2 playerPosition;
    Laser laser;
    Entity entity;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //playerPosition = Game.player.self.gameObject.transform.position;
        entity = animator.GetComponent<Entity>();
        laser = new Laser("Player");
        //laser.Attack(Angles.AngleBetweenVector2(animator.transform.position, playerPosition), entity);
        float angleOffset = Random.Range(-20, 21);
        laser.Attack(45 + angleOffset, entity);
        laser.Attack(135 + angleOffset, entity);
        laser.Attack(225 + angleOffset, entity);
        laser.Attack(315 + angleOffset, entity);
        animator.SetTrigger("idle");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("idle");
    }
}
