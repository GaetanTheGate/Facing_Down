using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	readonly List<Item> items;

	public Inventory() {
		items = new List<Item>();
	}

	public void AddItem(Item item) {
		items.Add(item);
		item.OnPickup();
	}

	public void RemoveItem(Item item) {
		items.Remove(item);
		item.OnRemove();
	}

	public List<Item> GetItems() {
		return items;
	}

	

}
