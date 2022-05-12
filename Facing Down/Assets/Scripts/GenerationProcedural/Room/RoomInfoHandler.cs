using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfoHandler : MonoBehaviour
{
    
    public void InitRoomInfo()
    {
        GetComponentInChildren<EnemyHandler>().Chose();
    }

    public void FinishRoom()
    {
        GetComponentInParent<RoomHandler>().OnFinishRoom();
    }

    public void SpawnEnemy()
    {
        GetComponentInChildren<EnemyHandler>().Spawn();
    }

    public void DespawnEnemy()
    {
        GetComponentInChildren<EnemyHandler>().Despawn();
    }

    public bool checkIfNoEnemy(){
        return GetComponentInChildren<EnemyHandler>().checkIfNoEnemy();
    }
}
