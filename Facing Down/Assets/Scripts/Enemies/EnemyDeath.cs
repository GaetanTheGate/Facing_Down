using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject parentToDestroy;

    protected bool isDead = false;
    protected Animator animator;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (isDead)
        {
            StartCoroutine(startWaitingRoutine());
        }
    }

    protected virtual void die()
    {
        GetComponent<GravityEntity>().gravity.setSpeed(9.8f);
        if (Game.currentRoom != null && GetComponent<RoomHandler>() != null) Game.currentRoom.GetComponent<RoomHandler>().CheckIfRoomIsFinish();
        BroadcastMessage("deathEvent");
        isDead = true;
    }

    protected IEnumerator startWaitingRoutine()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("dead", true);
        Destroy(parentToDestroy, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
