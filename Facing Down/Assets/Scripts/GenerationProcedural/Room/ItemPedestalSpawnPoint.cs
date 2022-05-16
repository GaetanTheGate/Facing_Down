using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPedestalSpawnPoint : PedestalSpawnPoint
{
    protected override void _Chose()
    {
        chosenItems = new List<ItemPedestal>();
        if (Game.random.Next(0, 101) > chanceToSpawn)
            return;


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
    }
}