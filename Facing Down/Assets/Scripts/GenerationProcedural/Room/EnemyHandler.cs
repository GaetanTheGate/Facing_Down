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
        foreach (EnemySpawnPoint spawnPoint in GetComponentsInChildren<EnemySpawnPoint>())
            spawnPoint.Chose();
    }

    public void Spawn()
    {


        foreach (EnemySpawnPoint spawnPoint in GetComponentsInChildren<EnemySpawnPoint>())
        {
            spawnPoint.Spawn();
        }
    }

    public void Despawn()
    {
        foreach (EnemySpawnPoint spawnPoint in GetComponentsInChildren<EnemySpawnPoint>())
            spawnPoint.Despawn();
    }
}
