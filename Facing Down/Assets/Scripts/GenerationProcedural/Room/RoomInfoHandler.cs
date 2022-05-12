using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfoHandler : MonoBehaviour
{
    private bool isOver = false;

    public void InitRoomInfo()
    {
        GetComponentInChildren<EnemyHandler>().Chose();
        GetComponentInChildren<PedestalHandler>().Chose();
    }

    public void FinishRoom()
    {
        isOver = true;
        GetComponentInParent<RoomHandler>().OnFinishRoom();
    }

    public void EnterRoom()
    {
        if(! isOver) GetComponentInChildren<EnemyHandler>().Spawn();
        GetComponentInChildren<PedestalHandler>().Spawn();
    }

    public void ExitRoom()
    {
        GetComponentInChildren<EnemyHandler>().Despawn();
        GetComponentInChildren<PedestalHandler>().Despawn();
    }

    public bool checkIfNoEnemy(){
        return GetComponentInChildren<EnemyHandler>().checkIfNoEnemy();
    }

    public void NoMoreEnemy()
    {
        FinishRoom();
    }
}
