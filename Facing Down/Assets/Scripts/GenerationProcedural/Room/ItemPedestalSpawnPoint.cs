using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPedestalSpawnPoint : PedestalSpawnPoint
{
    public ItemRarity minRarity = ItemRarity.COMMON;
    public ItemRarity maxRarity = ItemRarity.LEGENDARY;

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

            chosenItems = ItemChoice.SpawnItemChoice(transform.parent, spawnPoint, minRarity, maxRarity).GetItemPedestals();
        }
        else
        {
            List<ItemPedestal> list = new List<ItemPedestal>(itemPossibilityList);

            foreach (Transform point in GetComponentsInChildren<Transform>())
                chosenItems.Add(GetRandomItemFromList(list));
        }
    }
}