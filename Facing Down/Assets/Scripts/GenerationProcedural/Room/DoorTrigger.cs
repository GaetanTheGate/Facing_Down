using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            if (Game.currentRoom.Equals(GetComponentInParent<RoomHandler>()))
                return;

            Game.currentRoom.OnExitRoom();
            //Map.changeColorMapicon(Game.currentRoom.gameObject,GetComponentInParent<RoomHandler>().gameObject);
            GetComponentInParent<RoomHandler>().OnEnterRoom();
        }
        
    }
}
