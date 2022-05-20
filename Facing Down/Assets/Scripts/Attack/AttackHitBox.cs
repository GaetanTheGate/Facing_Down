using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    public float multiplier = 1.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponentInParent<AttackHit>().ComputeAttack(collision, multiplier);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GetComponentInParent<AttackHit>().ComputeAttack(collision, multiplier);
    }
}
