using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : ThrowableAttack
{
    public List<string> layersToDestroyOn = new List<string>();

    public int numberOfHitToDestroy = 1;
    private int numberOfHit = 0;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.GetComponent<Collider2D>().IsTouching(collision))
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            if (isUsingEndAnimation)
            {
                gameObject.GetComponent<Animator>().SetBool("isDead", true);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                Destroy(gameObject.GetComponent<ProjectileAttack>());
            }
            else
                Destroy(gameObject);

        foreach (string layer in layersToDestroyOn)
            if (collision.gameObject.layer.Equals(LayerMask.NameToLayer(layer)))
            {
                ++numberOfHit;
                if(numberOfHit >= numberOfHitToDestroy)
                {
                    if (isUsingEndAnimation)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        Destroy(gameObject.GetComponent<ProjectileAttack>());
                    }
                    else
                        Destroy(gameObject);
                }
                    
            }
    }
}
