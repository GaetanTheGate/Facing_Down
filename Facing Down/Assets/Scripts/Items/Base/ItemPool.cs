using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemPool
{
	private static Dictionary<string, Item> items;
    static ItemPool() {
		items = new Dictionary<string, Item>();

		add(new AttackUpItem());
		add(new AttackMultUpItem());
	}

	private static void add(Item item) {
		items.Add(item.getID(), item);
	}

	public static Item getByID(string id) {
		if (!items.ContainsKey(id)) return null;
		return items[id];
	}
}
