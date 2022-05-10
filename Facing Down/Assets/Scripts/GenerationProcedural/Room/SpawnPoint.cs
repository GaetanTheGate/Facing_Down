using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool canSpawn = true;
    public List<Transform> wayPoints;
    public List<GameObject> enemyList;

    private GameObject enemyChosen;
    private GameObject enemyEntity;

    public void ChoseEnemy()
    {
        enemyChosen = enemyList[Game.random.Next(0, enemyList.Count - 1)];
    }

    public void SpawnEnemy()
    {
        if(canSpawn){
           enemyEntity = Instantiate(enemyChosen);
            enemyEntity.transform.position = transform.position;
            enemyEntity.transform.SetParent(transform.parent);
            enemyEntity.GetComponent<EnemyMovement>().flags = wayPoints.ToArray(); 
        }
        
    }

    public void DespawnEnemy()
    {   
        if(canSpawn)
            Destroy(enemyEntity);
    }

}
