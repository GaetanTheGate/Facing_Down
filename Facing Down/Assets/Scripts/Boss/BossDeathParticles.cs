using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathParticles : MonoBehaviour
{
    Animator animator;
    public ParticleSystem particleEffectSparks;
    public ParticleSystem particleEffectExplosion;
    public ParticleSystem particleEffectFinalExplosion;
    public ParticleSystem pSystemSparks;
    public ParticleSystem pSystemExplosion;
    public ParticleSystem pSystemFinalExplosion;
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
            pSystemSparks = Instantiate(particleEffectSparks, transform.position, Quaternion.identity);
            pSystemExplosion = Instantiate(particleEffectExplosion, transform.position, Quaternion.identity);
        }
        if(!animator.GetBool("isDying"))
        {
            //ParticleSystem.MainModule main = pSystem.main;
            //main.loop = false;
            wasDoneOnce = false;
            //Destroy(gameObject);
        }
        if (animator.GetBool("isDyingFinalExplosion"))
        {
            pSystemFinalExplosion = Instantiate(particleEffectFinalExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
