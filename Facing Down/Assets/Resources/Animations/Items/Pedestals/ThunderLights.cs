using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderLights : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = false;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().enabled = true;
    }
}
