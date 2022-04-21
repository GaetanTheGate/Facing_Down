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
			items[item.getID()].modifyAmount(item.getAmount());
			UI.inventoryDisplay.update(item);
		}
		for (int i = 0; i < item.getAmount(); ++i)
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

	public float OnTakeDamage(float damage) {
		List<Item> delayedItems = new List<Item>();
		foreach (Item item in items.Values) {
			if (item.getPriority() == ItemPriority.LATE) delayedItems.Add(item);
			else damage = item.OnTakeDamage(damage);
		}
		foreach (Item item in delayedItems) {
			damage = item.OnTakeDamage(damage);
		}
		return damage;
	}

	public void OnDeath() {
		foreach (Item item in items.Values) {
			if (item.OnDeath()) break;
		}
	}

	public Dictionary<string, Item> GetItems() {
		return items;
	}
}
