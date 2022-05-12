using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour, SpawnPoint
{
    private int enemyLeft = 0;

    public void EnemyKilled()
    {
        enemyLeft -= 1;
        if(enemyLeft == 0) GetComponentInParent<RoomInfoHandler>().NoMoreEnemy();
    }

    public void EnemyAdded()
    {
        enemyLeft += 1;
    }

    public bool checkIfNoEnemy(){
        print("checkIfNoEnemy");
        foreach(Transform child in transform){
            if(child.CompareTag("Enemy") && !child.GetComponent<StatEntity>().getIsDead()){
                print(child.name + " isDead ? " + child.GetComponent<StatEntity>().getIsDead());
                return false;
            }
                
        }
        return true;
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
