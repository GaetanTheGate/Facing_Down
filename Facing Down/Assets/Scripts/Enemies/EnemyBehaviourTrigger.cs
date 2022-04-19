using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTrigger : MonoBehaviour
{
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StatPlayer statPlayer = collision.GetComponentInParent<StatPlayer>();
            statPlayer.takeDamage(damage);
        }
    }
}
