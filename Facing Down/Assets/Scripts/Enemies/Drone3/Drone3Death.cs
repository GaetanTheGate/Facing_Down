using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone3Death : MonoBehaviour
{
    private bool isDead = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isDead)
        {
            animator.SetBool("dead", true);
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    public void die()
    {
        BroadcastMessage("deathEvent");
        isDead = true;
    }
}
