using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour, SpawnPoint
{
    public List<Transform> wayPoints;
    public List<GameObject> enemyList;

    private GameObject enemyChosen;
    private GameObject enemyEntity;

    public void Chose()
    {
        enemyChosen = enemyList[Game.random.Next(0, enemyList.Count - 1)];
        wayPoints = new List<Transform>();
        for (int i = 0; i < transform.childCount; ++i)
            wayPoints.Add(transform.GetChild(i).transform);
    }

    public void Spawn()
    {
        enemyEntity = Instantiate(enemyChosen);
        enemyEntity.transform.position = transform.position;
        enemyEntity.transform.SetParent(transform.parent);
        enemyEntity.GetComponent<EnemyMovement>().flags = wayPoints.ToArray();


        GetComponentInParent<EnemyHandler>().EnemyAdded();
        enemyEntity.GetComponent<StatEntity>().onDeath.AddListener(OnEnemyDeath);
    }

    public void Despawn()
    {
        Destroy(enemyEntity);
    }

    private void OnEnemyDeath()
    {
        GetComponentInParent<EnemyHandler>().EnemyKilled();
    }
}
