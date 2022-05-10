using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCyborgDeath : MonoBehaviour
{
    public GameObject parentToDestroy;

    private bool isDead = false;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isDead)
        {
            StartCoroutine(startWaitingRoutine());
        }
    }

    public void die()
    {
        GetComponent<GravityEntity>().gravity.setSpeed(9.8f);
        if (Game.currentRoom != null && GetComponent<RoomHandler>() != null) Game.currentRoom.GetComponent<RoomHandler>().CheckIfRoomIsFinish();
        BroadcastMessage("deathEvent");
        isDead = true;
    }

    private IEnumerator startWaitingRoutine()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("dead", true);
        Destroy(parentToDestroy, animator.GetCurrentAnimatorStateInfo(0).length);
    }
}
