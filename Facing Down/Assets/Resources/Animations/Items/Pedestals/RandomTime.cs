using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTime : StateMachineBehaviour
{
    [Min(0)]public float minTime = 0.05f;
    [Min(0)]public float maxTime = 0.10f;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.speed = 1 / Random.Range(minTime, maxTime);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.speed = 1;
    }
}
