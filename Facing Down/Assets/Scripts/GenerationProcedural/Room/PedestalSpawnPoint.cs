using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalSpawnPoint : MonoBehaviour, SpawnPoint
{
    List<ItemPedestal> itemPossibilityList;

    List<ItemPedestal> chosenItems;

    public void Chose()
    {
        chosenItems = new List<ItemPedestal>();

        if (itemPossibilityList == null || itemPossibilityList.Count == 0)
        {
            List<Vector2> spawnPoint = new List<Vector2>();
            foreach (Transform point in GetComponentsInChildren<Transform>())
                spawnPoint.Add(point.position);
            chosenItems = new ItemChoice(transform.parent, spawnPoint).GetItemPedestals();
        }
        else
        {
            List<ItemPedestal> list = new List<ItemPedestal>(itemPossibilityList);

            foreach (Transform point in GetComponentsInChildren<Transform>())
                chosenItems.Add(GetRandomItemFromList(list));
        }

        Despawn();
    }

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

    private T GetRandomItemFromList<T>(List<T> list)
    {
        T _object = list[Game.random.Next(0, list.Count - 1)];
        list.Remove(_object);

        return _object;
    }
}
