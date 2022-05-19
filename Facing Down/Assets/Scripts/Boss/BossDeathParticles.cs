using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathParticles : MonoBehaviour
{
    Animator animator;
    public ParticleSystem particleEffect;
    ParticleSystem pSystem;
    bool wasDoneOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!wasDoneOnce && animator.GetBool("isDying"))
        {
            wasDoneOnce = true;
            pSystem = Instantiate(particleEffect, transform.position, Quaternion.identity);
        }
        if(pSystem != null && !animator.GetBool("isDying"))
        {
            ParticleSystem.MainModule main = pSystem.main;
            main.loop = false;
            wasDoneOnce = false;
            Destroy(gameObject);
        }
    }
}
