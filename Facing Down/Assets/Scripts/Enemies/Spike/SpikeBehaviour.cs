using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
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
            StatPlayer statPlayer = collision.GetComponent<StatPlayer>();
            statPlayer.TakeDamage(new DamageInfo(null, statPlayer.gameObject.GetComponent<Entity>(), damage));
        }
    }
}
