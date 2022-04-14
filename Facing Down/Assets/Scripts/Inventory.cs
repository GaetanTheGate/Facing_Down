using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
	readonly List<Item> Items;

	public Inventory() {
		Items = new List<Item>();
	}

	public void AddItem(Item Item) {
		Items.Add(Item);
		Item.OnPickup();
	}

	public List<Item> GetItems() {
		return Items;
	}

	

}
