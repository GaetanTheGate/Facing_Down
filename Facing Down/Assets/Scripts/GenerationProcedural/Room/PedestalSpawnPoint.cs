using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalSpawnPoint : MonoBehaviour, SpawnPoint
{
    List<GameObject> itemPossibilityList;

    Dictionary<Transform, GameObject> chosenItems;

    public void Chose()
    {
        chosenItems = new Dictionary<Transform, GameObject>();

        if (itemPossibilityList == null || itemPossibilityList.Count == 0)
        {

        }
        else
        {
            foreach (Transform point in GetComponentsInChildren<Transform>())
            {
                chosenItems.Add(point, GetRandomItemFromList());
            }
        }
    }

    public void Spawn()
    {

    }

    public void Despawn()
    {

    }

    private GameObject GetRandomItemFromList()
    {
        GameObject _object = itemPossibilityList[Game.random.Next(0, itemPossibilityList.Count - 1)];
        while (chosenItems.ContainsValue(_object))
            _object = itemPossibilityList[Game.random.Next(0, itemPossibilityList.Count - 1)];

        return _object;
    }
}
