using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PedestalSpawnPoint : MonoBehaviour, SpawnPoint
{
    [Range(0,100)] public int chanceToSpawn = 100;

    protected List<ItemPedestal> itemPossibilityList;

    protected List<ItemPedestal> chosenItems;

    public void Chose()
    {
        _Chose();
        Despawn();
    }

    protected abstract void _Chose();

    public void Spawn()
    {
        foreach (ItemPedestal pedestal in chosenItems)
            pedestal.gameObject.SetActive(true);
    }

    public void Despawn()
    {
        foreach (ItemPedestal pedestal in chosenItems)
            pedestal.gameObject.SetActive(false);
    }

    protected T GetRandomItemFromList<T>(List<T> list)
    {
        T _object = list[Game.random.Next(0, list.Count - 1)];
        list.Remove(_object);

        return _object;
    }
}
