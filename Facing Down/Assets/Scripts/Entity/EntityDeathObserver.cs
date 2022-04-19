using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDeathObserver : MonoBehaviour
{
    public Animator animator;
    private StatEntity statEntity;

    private void Start()
    {
        statEntity = gameObject.GetComponent<StatEntity>();
        animator = gameObject.GetComponent<Animator>();
        if (animator != null) animator.SetFloat("hp", statEntity.hitPoints);
    }

    private void OnEnable()
    {
        StatEntity.onHit += doOnHit;
    }

    private void OnDisable()
    {
        StatEntity.onHit -= doOnHit;
    }

    //actions à faire quand l'entité prend des dégats
    private void doOnHit(int damage)
    {
        if (animator != null) animator.SetFloat("hp", statEntity.hitPoints);
        if (statEntity.hitPoints <= 0)
        {

        }
    }
}
