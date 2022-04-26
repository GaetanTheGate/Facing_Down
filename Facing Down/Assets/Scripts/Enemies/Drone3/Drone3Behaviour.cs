using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone3Behaviour : MonoBehaviour
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
                statPlayer.takeDamage(damage);
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
