using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChoice {
    ItemPedestal firstItem;
    ItemPedestal secondItem;

    public ItemChoice(GameObject parent, Vector2 position1, Vector2 position2) {
        firstItem = ItemPedestal.SpawnRandomItemPedestal(parent, position1);
        secondItem = ItemPedestal.SpawnRandomItemPedestal(parent, position2);
        firstItem.SetItemChoice(this);
        secondItem.SetItemChoice(this);
    }

    public void DisablePedestals() {
        firstItem.DisablePedestal();
        secondItem.DisablePedestal();
    }
}