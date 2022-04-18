using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemDisplay itemDisplay;
    private readonly Vector2 ROOT_POSITION = new Vector2(Screen.width / 20, Screen.height * 7 / 8);
    private readonly Vector2 X_OFFSET = new Vector2(Screen.width / 20, 0);
    private readonly Vector2 Y_OFFSET = new Vector2(0, -Screen.width / 20);
    private readonly int ROW_SIZE = 18;

    readonly Dictionary<string, ItemDisplay> itemDisplays = new Dictionary<string, ItemDisplay>();


    public void addItemDisplay(Item item) {
        ItemDisplay newItemDisplay = Instantiate<ItemDisplay>(itemDisplay);
        newItemDisplay.transform.SetParent(transform);
        newItemDisplay.Init(item);
        newItemDisplay.setPosition(ROOT_POSITION + X_OFFSET * (itemDisplays.Count % 18) + Y_OFFSET * (itemDisplays.Count / 18));
        itemDisplays.Add(item.getID(), newItemDisplay);
	}

    public void removeItemDisplay(Item item) {
        Debug.Log("REMOVING");
        Destroy(itemDisplays[item.getID()]);
        itemDisplays.Remove(item.getID());
        int index = 0;
        foreach (string ID in itemDisplays.Keys) {
            itemDisplays[ID].setPosition(ROOT_POSITION + X_OFFSET * (index % 18) + Y_OFFSET * (index / 18));
            index += 1;
        }
	}

    public void update(Item item) {
        itemDisplays[item.getID()].update();
	}
}
