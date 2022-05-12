using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public void ChoseEnemy()
    {
        foreach (SpawnPoint spawnPoint in GetComponentsInChildren<SpawnPoint>())
            spawnPoint.ChoseEnemy();
    }

    public void SpawnEnemy()
    {
        foreach (SpawnPoint spawnPoint in GetComponentsInChildren<SpawnPoint>())
            spawnPoint.SpawnEnemy();
    }

    public void DespawnEnemy()
    {
        foreach (SpawnPoint spawnPoint in GetComponentsInChildren<SpawnPoint>())
            spawnPoint.DespawnEnemy();
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

}
