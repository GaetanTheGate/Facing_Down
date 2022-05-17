using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeLaserParticles : MonoBehaviour
{
    public ParticleSystem particleEffect;
    private ParticleSystem pSystem;
    private Animator animator;
    bool wasDoneOnce = false;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void launchChargingParticles()
    {
        pSystem = Instantiate(particleEffect, transform.position, Quaternion.identity);
    }

    private void FixedUpdate()
    {
        if(!wasDoneOnce && animator.GetBool("isChargingLaser") == true)
        {
            wasDoneOnce = true;
            launchChargingParticles();
        }
        if(pSystem != null && animator.GetBool("isChargingLaser") == false)
        {
            pSystem.Stop();
            wasDoneOnce = false;
        }
    }
}
