using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Links multiple ItemPedestal to allow only one item to be picked from them
/// </summary>
public class ItemChoice {
    private bool active;
    private List<ItemPedestal> pedestals;

    public ItemChoice (List<ItemPedestal> pedestals) {
        active = true;
        this.pedestals = pedestals;
        foreach (ItemPedestal pedestal in this.pedestals) {
            pedestal.SetItemChoice(this);
		}
    }


    /// <summary>
    /// Creates rendom pedestals linked by a choice using the given parent and positions. One pedestal is created at each position.
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="positions"></param>
    public static ItemChoice SpawnItemChoice (Transform parent, List<Vector2> positions) {
        List<ItemPedestal> pedestals = new List<ItemPedestal>();
        foreach (Vector2 position in positions) {
            pedestals.Add(ItemPedestal.SpawnRandomItemPedestal(parent, position));
		}
        return new ItemChoice(pedestals);
    }

    public static ItemChoice SpawnWeaponChoice(Transform parent, List<Vector2> positions) {
        List<ItemPedestal> pedestals = new List<ItemPedestal>();
        foreach (Vector2 position in positions) {
            pedestals.Add(ItemPedestal.SpawnRandomWeaponPedestal(parent, position));
        }
        return new ItemChoice(pedestals);
    }

    /// <summary>
    /// Disables all pedestal belonging to this itemChoice.
    /// </summary>
    public void DisablePedestals() {
        if (!active) return;
        active = false;
        foreach (ItemPedestal itemPedestal in pedestals) itemPedestal.DisablePedestal();
    }

    public List<ItemPedestal> GetItemPedestals() {
        return pedestals;
    }
}