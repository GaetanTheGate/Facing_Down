using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// InventoryDisplay manages the inventory display on the UI.
/// </summary>
public class InventoryDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemDisplay itemDisplay;
    private readonly Vector2 ROOT_POSITION = new Vector2(Screen.width / 20, Screen.height * 7 / 8);
    private readonly Vector2 X_OFFSET = new Vector2(Screen.width / 20, 0);
    private readonly Vector2 Y_OFFSET = new Vector2(0, -Screen.width / 20);
    private readonly int ROW_SIZE = 18;

    readonly Dictionary<string, ItemDisplay> itemDisplays = new Dictionary<string, ItemDisplay>();

    /// <summary>
    /// Adds a new item to the display.
    /// </summary>
    /// <param name="item">The Item to add. Should be an item from the player's inventory.</param>
    public void AddItemDisplay(Item item) {
        ItemDisplay newItemDisplay = Instantiate<ItemDisplay>(itemDisplay);
        newItemDisplay.transform.SetParent(transform);
        newItemDisplay.Init(item);
        newItemDisplay.SetPosition(ROOT_POSITION + X_OFFSET * (itemDisplays.Count % ROW_SIZE) + Y_OFFSET * (itemDisplays.Count / ROW_SIZE));
        itemDisplays.Add(item.GetID(), newItemDisplay);
	}

    /// <summary>
    /// Removes an item from the display. Should only be called from the inventory.
    /// </summary>
    /// <param name="item">The item to be removed. Should be an item from the player's inventory.</param>
    public void RemoveItemDisplay(Item item) {
        Destroy(itemDisplays[item.GetID()].gameObject);
        itemDisplays.Remove(item.GetID());
        int index = 0;
        foreach (string ID in itemDisplays.Keys) {
            itemDisplays[ID].SetPosition(ROOT_POSITION + X_OFFSET * (index % 18) + Y_OFFSET * (index / 18));
            index += 1;
        }
	}

    /// <summary>
    /// Updates an item. Should only be called from the inventory.
    /// </summary>
    /// <param name="item">The item to update. Should be an item from the player's inventory.</param>
    public void UpdateItemDisplay(Item item) {
        itemDisplays[item.GetID()].UpdateDisplay();
	}
}
