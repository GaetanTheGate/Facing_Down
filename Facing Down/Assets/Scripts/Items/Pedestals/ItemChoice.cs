using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Links multiple ItemPedestal to allow only one item to be picked from them
/// </summary>
public class ItemChoice {
    private bool active;
    private List<ItemPedestal> itemPedestals;


    /// <summary>
    /// Creates rendom pedestals linked by a choice using the given parent and positions. One pedestal is created at each position.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="positions"></param>
    public ItemChoice(Transform parent, List<Vector2> positions) {
        active = true;
        itemPedestals = new List<ItemPedestal>();
        foreach (Vector2 position in positions) {
            ItemPedestal itemPedestal = ItemPedestal.SpawnRandomItemPedestal(parent, position);
            itemPedestal.SetItemChoice(this);
            itemPedestals.Add(itemPedestal);
		}
    }

    /// <summary>
    /// Disables all pedestal belonging to this itemChoice.
    /// </summary>
    public void DisablePedestals() {
        if (!active) return;
        active = false;
        foreach (ItemPedestal itemPedestal in itemPedestals) itemPedestal.DisablePedestal();
    }

    public List<ItemPedestal> GetItemPedestals() {
        return itemPedestals;
    }
}