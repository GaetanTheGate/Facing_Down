using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	readonly Dictionary<string, Item> items;

	public Inventory() {
		items = new Dictionary<string, Item>();
	}

	public void AddItem(Item item) {
		if (!items.ContainsKey(item.getID())) {
			items.Add(item.getID(), item);
			UI.inventoryDisplay.addItemDisplay(item);
		}
		else {
			items[item.getID()].modifyAmount(1);
			UI.inventoryDisplay.update(item);
		}
		item.OnPickup();
	}

	public void RemoveItem(Item item) {
		items[item.getID()].modifyAmount(-1);
		if (items[item.getID()].getAmount() == 0) {
			items.Remove(item.getID());
			UI.inventoryDisplay.removeItemDisplay(item);
		}
		else {
			UI.inventoryDisplay.update(item);
		}
		item.OnRemove();
	}

	public Dictionary<string, Item> GetItems() {
		return items;
	}
}
