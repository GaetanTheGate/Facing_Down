using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChoice {
    private bool active;
    private ItemPedestal firstItem;
    private ItemPedestal secondItem;


    public ItemChoice(GameObject parent, Vector2 position1, Vector2 position2) {
        active = true;
        firstItem = ItemPedestal.SpawnRandomItemPedestal(parent, position1);
        secondItem = ItemPedestal.SpawnRandomItemPedestal(parent, position2);
        firstItem.SetItemChoice(this);
        secondItem.SetItemChoice(this);
    }

    public void DisablePedestals() {
        if (!active) return;
        active = false;
        firstItem.DisablePedestal();
        secondItem.DisablePedestal();
    }
}