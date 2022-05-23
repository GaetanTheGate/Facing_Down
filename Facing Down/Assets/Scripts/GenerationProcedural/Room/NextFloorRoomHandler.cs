using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFloorRoomHandler : BaseRoomHandler
{
    public override void OnEnterRoom()
    {
        Tower.generateNextFloor();
    }
}
