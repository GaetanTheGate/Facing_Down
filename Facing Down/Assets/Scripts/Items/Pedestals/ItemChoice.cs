using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChoice {
    private bool active;
    private List<ItemPedestal> itemPedestals;


    public ItemChoice(GameObject parent, List<Vector2> positions) {
        active = true;
        itemPedestals = new List<ItemPedestal>();
        foreach (Vector2 position in positions) {
            ItemPedestal itemPedestal = ItemPedestal.SpawnRandomItemPedestal(parent, position);
            itemPedestal.SetItemChoice(this);
            itemPedestals.Add(itemPedestal);
		}
    }

    public void DisablePedestals() {
        if (!active) return;
        active = false;
        foreach (ItemPedestal itemPedestal in itemPedestals) itemPedestal.DisablePedestal();
    }

    public List<ItemPedestal> GetItemPedestals() {
        return itemPedestals;
    }
}