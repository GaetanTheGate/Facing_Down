using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnPoint : MonoBehaviour, SpawnPoint
{
    public List<GameObject> enemyList;

    private GameObject enemyChosen;
    private GameObject enemyEntity;

    public void Chose()
    {
        enemyChosen = enemyList[Game.random.Next(0, enemyList.Count - 1)];
    }

    public void Spawn()
    {
        enemyEntity = Instantiate(enemyChosen);
        enemyEntity.transform.position = transform.position;
        enemyEntity.transform.SetParent(transform.parent);


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
