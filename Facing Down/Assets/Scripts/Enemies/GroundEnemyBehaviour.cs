using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyBehaviour : MonoBehaviour
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerBehaviour playerBehaviour = collision.collider.GetComponent<PlayerBehaviour>();
            playerBehaviour.getHit(damage);
        }
    }
}
