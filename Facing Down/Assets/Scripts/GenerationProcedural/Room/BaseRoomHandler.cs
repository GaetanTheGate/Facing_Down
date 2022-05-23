using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRoomHandler : MonoBehaviour
{
    public virtual void OnEnterRoom()
    {
        GetComponentInParent<RoomHandler>().OnEnterRoom();
    }
}
