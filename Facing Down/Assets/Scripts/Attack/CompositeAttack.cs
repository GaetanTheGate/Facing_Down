using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositeAttack : Attack
{
    public List<Attack> attackList = new List<Attack>();

    protected override void ComputeAttack(float percentageTime)
    {
    }

    protected override void onStart()
    {
        startDelay = 0.0f;
        timeSpan = 0.0f;
        endDelay = 0.0f;
        foreach(Attack attack in attackList)
        {
            attack.startAttack();
        }
    }
}
