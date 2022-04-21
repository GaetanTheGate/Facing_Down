using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBehaviour : MonoBehaviour
{
    public int damage = 1;
    private bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isActive)
        {
            if (collision.collider.CompareTag("Player"))
            {
                StatPlayer statPlayer = collision.collider.GetComponentInParent<StatPlayer>();
                statPlayer.takeDamage(damage);
            }
        }
    }

    public void disableBehaviour()
    {
        isActive = false;
        enabled = false;
    }
}
