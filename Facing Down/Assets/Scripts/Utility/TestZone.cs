using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (RoomHandler roomHandler in GetComponentsInChildren<RoomHandler>())
            roomHandler.InitRoom("basic");
        RoomHandler room = GetComponentInChildren<RoomHandler>();
        room.SetAsStart();
        Game.player.self.transform.position = room.transform.position;
        
        AstarPath.active.Scan();
        ShadowCaster2DFromComposite.RebuildAll();
    }
}
