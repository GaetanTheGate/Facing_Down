using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCyborgBehaviour : MonoBehaviour
{
    public int damage = 1;
    private bool isActive = true;

    private void OnTriggerStay2D(Collider2D collision)
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

    private void deathEvent()
    {
        disableBehaviour();
    }

    public void disableBehaviour()
    {
        isActive = false;
        enabled = false;
    }
}
