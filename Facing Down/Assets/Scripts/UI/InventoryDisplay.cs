using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// InventoryDisplay manages the inventory display on the UI.
/// </summary>
public class InventoryDisplay : MonoBehaviour
{
    private GameObject itemDisplayPrefab;
    private Dictionary<string, GameObject> itemDisplays;

    public int ROW_SIZE = 18;
    public float offset = 80f;


	private void Start() {
        itemDisplayPrefab = Resources.Load<GameObject>("Prefabs/UI/Components/ItemDisplay");
        itemDisplays = new Dictionary<string, GameObject>();
	}

	/// <summary>
	/// Adds a new item to the display.
	/// </summary>
	/// <param name="item">The Item to add. Should be an item from the player's inventory.</param>
	public void AddItemDisplay(Item item) {
        GameObject itemDisplay = InstantiateItemDisplay(item);
        itemDisplay.transform.localPosition = new Vector2(itemDisplays.Count % ROW_SIZE * offset, itemDisplays.Count / ROW_SIZE * offset);
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
        itemDisplays[item.GetID()].GetComponentInChildren<Text>().text = item.GetAmount().ToString();
	}

    private GameObject InstantiateItemDisplay(Item item) {
        GameObject itemDisplay = Instantiate<GameObject>(itemDisplayPrefab, transform);
        itemDisplay.GetComponentInChildren<Image>().sprite = item.GetSprite();
        itemDisplay.GetComponentInChildren<Text>().text = item.GetAmount().ToString();
        return itemDisplay;
    }
}
