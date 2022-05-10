using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfoHandler : MonoBehaviour
{
    
    public void InitRoomInfo()
    {
        GetComponentInChildren<EnemyHandler>().ChoseEnemy();
    }

    public void FinishRoom()
    {
        GetComponentInParent<RoomHandler>().OnFinishRoom();
    }

    public void SpawnEnemy()
    {
        GetComponentInChildren<EnemyHandler>().SpawnEnemy();
    }

    public void DespawnEnemy()
    {
        GetComponentInChildren<EnemyHandler>().DespawnEnemy();
    }
}
