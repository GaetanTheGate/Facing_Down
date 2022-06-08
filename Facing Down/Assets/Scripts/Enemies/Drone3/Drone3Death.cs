using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone3Death : EnemyDeath
{
    private AudioClip deathAudio;

    protected override void Start()
    {
        base.Start();
        deathAudio = Resources.Load<AudioClip>("Sound_Effects/engine_stop");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isDead)
        {
            StartCoroutine(startWaitingRoutine());
        }
    }

    public override void die()
    {
        base.die();
        GetComponent<GravityEntity>().gravity.setSpeed(9.8f);
        GameObject droneAudio = new GameObject("Drone Audio");
        droneAudio.transform.parent = transform;
        droneAudio.AddComponent<AudioSource>();

        droneAudio.GetComponent<AudioSource>().volume = 0.5f;
        droneAudio.GetComponent<AudioSource>().PlayOneShot(deathAudio);
    }

    private IEnumerator startWaitingRoutine()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        explode();
        Destroy(parentToDestroy);
    }
}
