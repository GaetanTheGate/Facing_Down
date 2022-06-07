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

    public virtual void die()
    {
        //if (Game.currentRoom != null && GetComponent<RoomHandler>() != null) Game.currentRoom.GetComponent<RoomHandler>().CheckIfRoomIsFinish();
        //BroadcastMessage("deathEvent");
        isDead = true;

        if(GetComponent<Entity>() != null)
        {
            print("total = " + GetComponent<Entity>().attacks.Count);
            foreach (Attack attack in GetComponent<Entity>().attacks)
            {
                if (attack is MeleeAttack) Destroy(attack.gameObject);
            }
        }
    }
}
