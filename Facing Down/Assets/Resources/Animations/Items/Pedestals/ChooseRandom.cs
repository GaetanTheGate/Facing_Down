using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRandom : StateMachineBehaviour
{
    public string nameOfVar = "choice";
    [Min(0)] public int numberOfChoice = 2;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int lastChoice = animator.GetInteger("choice");
        animator.SetInteger("choice", getRandomExclude(lastChoice));
    }

    private int getRandomExclude(int lastChoice)
    {
        int random = lastChoice;
        while (random == lastChoice)
            random = Random.Range(0, numberOfChoice);
        return random;
    }
}
