using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int damage = 1;
    protected bool isActive = true;

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (isActive)
        {
            if (collision.CompareTag("Player"))
            {
                StatPlayer statPlayer = collision.GetComponent<StatPlayer>();
                statPlayer.TakeDamage(new DamageInfo(gameObject.GetComponent<Entity>(), statPlayer.gameObject.GetComponent<Entity>(), damage));
            }
        }
    }

    /*protected virtual void deathEvent()
    {
        disableBehaviour();
    }*/

    public void disableBehaviour()
    {
        isActive = false;
        enabled = false;
    }
}
