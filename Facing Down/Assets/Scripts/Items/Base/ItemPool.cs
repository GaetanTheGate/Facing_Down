using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemPool
{
	private static Dictionary<ItemRarity, Dictionary<string, Item>> items;
	private static Dictionary<ItemRarity, int> rarityDistribution;
	private static int totalRarityWeight;
	private static System.Random random;
    static ItemPool() {
		random = new System.Random(Random.Range(int.MinValue, int.MaxValue));
		items = new Dictionary<ItemRarity, Dictionary<string, Item>>();
		rarityDistribution = new Dictionary<ItemRarity, int> { {ItemRarity.COMMON, 7}, {ItemRarity.UNCOMMON, 6}, {ItemRarity.RARE, 4}, {ItemRarity.EPIC, 2}, {ItemRarity.LEGENDARY, 1} };
		computeTotalRarityWeight();

		add(new AttackUpItem());
		add(new FieryAlloy());
		add(new StoneArmor());
	}

	public static void InitSeed(int s) {
		random = new System.Random(s);
	}

	private static void add(Item item) {
		if (!items.ContainsKey(item.GetRarity())) items.Add(item.GetRarity(), new Dictionary<string, Item>());
		items[item.GetRarity()].Add(item.getID(), item);
	}

	public static Item getByID(string id) {
		foreach (Dictionary<string, Item> rarityPool in items.Values) {
			if (rarityPool.ContainsKey(id)) return rarityPool[id].makeCopy();
		}
		return null;
	}

	private static void computeTotalRarityWeight() {
		totalRarityWeight = 0;
		foreach (int weight in rarityDistribution.Values) totalRarityWeight += weight;
	}

	public static ItemRarity getRandomRarity() {
		int r = random.Next() % totalRarityWeight;
		foreach(ItemRarity rarity in rarityDistribution.Keys) {
			r -= rarityDistribution[rarity];
			if (r < 0) return rarity;
		}
		Debug.Log("ERROR");
		return ItemRarity.COMMON; // Should never reach this, but the IDE detects an error if it's not there
	}

	public static Item getRandomItem() {
		List<Item> rarityPool = new List<Item>(items[getRandomRarity()].Values);
		return rarityPool[random.Next() % rarityPool.Count];
	}
}
