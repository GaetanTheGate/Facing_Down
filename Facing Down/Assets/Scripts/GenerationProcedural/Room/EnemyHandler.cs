using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour, SpawnPoint
{
    private int enemyLeft = 0;

    public int GetEnemyLeft() => enemyLeft;

    public void EnemyKilled()
    {
        enemyLeft -= 1;
        if(checkIfNoEnemy()) GetComponentInParent<RoomInfoHandler>().NoMoreEnemy();
    }

    public void EnemyAdded()
    {
        enemyLeft += 1;
    }

    public bool checkIfNoEnemy(){
        return enemyLeft == 0;
    }

    public void Chose()
    {
        foreach(SpawnPoint spawnPoint in GetComponentsInChildren<SpawnPoint>())
            if( ! spawnPoint.Equals(this)) spawnPoint.Chose();
    }

    public void Spawn()
    {
        foreach (SpawnPoint spawnPoint in GetComponentsInChildren<SpawnPoint>())
            if (!spawnPoint.Equals(this)) spawnPoint.Spawn();
    }

    public void Despawn()
    {
        foreach (SpawnPoint spawnPoint in GetComponentsInChildren<SpawnPoint>())
            if (!spawnPoint.Equals(this)) spawnPoint.Despawn();
    }
}
