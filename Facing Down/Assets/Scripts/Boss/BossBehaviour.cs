using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : EnemyBehaviour
{
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive)
        {
            if (collision.CompareTag("Player"))
            {
                StatPlayer statPlayer = collision.GetComponent<StatPlayer>();
                statPlayer.TakeDamage(new DamageInfo(gameObject.GetComponent<Entity>(), statPlayer.gameObject.GetComponent<Entity>(), damage, new Velocity(5f, Vector2.Angle(gameObject.transform.position, collision.transform.position))));
            }
        }
    }
}
