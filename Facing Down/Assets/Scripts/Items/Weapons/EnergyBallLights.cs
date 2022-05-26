using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallLights : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().pointLightOuterRadius = 1f;
        animator.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = 1f;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().pointLightOuterRadius = 1.2f;
        animator.GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>().intensity = 1.2f;
    }
}
