using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            Game.currentRoom.OnExitRoom();
            GetComponentInParent<RoomHandler>().OnEnterRoom();
        }
    }
}
