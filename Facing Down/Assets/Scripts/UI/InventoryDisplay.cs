using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// InventoryDisplay manages the inventory display on the UI.
/// </summary>
public class InventoryDisplay : MonoBehaviour
{
    private GameObject display;
    private Dictionary<string, ItemDisplay> itemDisplays;

    public int ROW_SIZE = 18;
    public float offset = 80f;


	private void Start() {
        display = transform.Find("Display").gameObject;
        itemDisplays = new Dictionary<string, ItemDisplay>();
	}

	/// <summary>
	/// Adds a new item to the display.
	/// </summary>
	/// <param name="item">The Item to add. Should be an item from the player's inventory.</param>
	public void AddItemDisplay(Item item) {
        ItemDisplay itemDisplay = ItemDisplay.InstantiateItemDisplay(item, display.transform, new Vector2(itemDisplays.Count % ROW_SIZE * offset, itemDisplays.Count / ROW_SIZE * offset));
        itemDisplays.Add(item.GetID(), itemDisplay);
	}

    /// <summary>
    /// Removes an item from the display. Should only be called from the inventory.
    /// </summary>
    /// <param name="item">The item to be removed. Should be an item from the player's inventory.</param>
    public void RemoveItemDisplay(Item item) {
        Destroy(itemDisplays[item.GetID()]);
        itemDisplays.Remove(item.GetID());
        int index = 0;
        foreach (string ID in itemDisplays.Keys) {
            itemDisplays[ID].transform.localPosition = new Vector2(index % ROW_SIZE * offset, index / ROW_SIZE * offset);
            index += 1;
        }
	}

    /// <summary>
    /// Updates an item. Should only be called from the inventory.
    /// </summary>
    /// <param name="item">The item to update. Should be an item from the player's inventory.</param>
    public void UpdateItemDisplay(Item item) {
        itemDisplays[item.GetID()].UpdateAmount(); ;
	}

    public void Enable() {
        display.SetActive(true);
	}

	public void Disable() {
        display.SetActive(false);
	}

    public bool IsEnabled() {
        return display.activeSelf;
	}
}
