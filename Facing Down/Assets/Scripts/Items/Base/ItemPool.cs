using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ItemPool registers all the Items.
/// </summary>
public static class ItemPool
{
	private static Dictionary<ItemRarity, Dictionary<string, PassiveItem>> items;
	private static Dictionary<ItemRarity, int> rarityDistribution; //Weights for each rarity.

	/// <summary>
	/// Initializes values
	/// </summary>
	/// New Items should be added here.
    static ItemPool() {
		items = new Dictionary<ItemRarity, Dictionary<string, PassiveItem>>();
		rarityDistribution = new Dictionary<ItemRarity, int> { {ItemRarity.COMMON, 7}, {ItemRarity.UNCOMMON, 6}, {ItemRarity.RARE, 4}, {ItemRarity.EPIC, 2}, {ItemRarity.LEGENDARY, 1} };

		Add(new FieryAlloy());
		Add(new MagmaticCoating());
		Add(new StoneArmor());
		Add(new ImpenetrableRoots());
		Add(new ArtificialWings());
		Add(new ClayClone());
		Add(new VoraciousFlames());
		Add(new HeartOfTheStorm());
		Add(new FocusAmber());
		Add(new BatteryPack());
		Add(new HardeningPlates());
		Add(new SylvanBreastplate());
		Add(new Lightness());
		Add(new InvisibilityCloak());
		Add(new ObsidianShard());
		Add(new RiftMaker());
		Add(new PowerSaver());
		//Add(new OverchargedCoil());
		Add(new BurningCircle());
		Add(new HornOfPlenty());
	}

	/// <summary>
	/// Adds an Item to the pool.
	/// </summary>
	/// <param name="item">The Item to add.</param>
	private static void Add(PassiveItem item) {
		if (!items.ContainsKey(item.GetRarity())) items.Add(item.GetRarity(), new Dictionary<string, PassiveItem>());
		items[item.GetRarity()].Add(item.GetID(), item);
	}

	/// <summary>
	/// Returns the item correspoding to an ID.
	/// </summary>
	/// <param name="id">The item's ID.</param>
	/// <returns>The item if found, else returns null.</returns>
	public static PassiveItem GetByID(string id) {
		foreach (Dictionary<string, PassiveItem> rarityPool in items.Values) {
			if (rarityPool.ContainsKey(id)) return (PassiveItem) rarityPool[id].MakeCopy();
		}
		return null;
	}

	/// <summary>
	/// Initializes totalRarityWeight from rarityDistribution.
	/// </summary>
	private static int ComputeTotalRarityWeight(ItemRarity minRarity, ItemRarity maxRarity) {
		int totalRarityWeight = 0;
		for (ItemRarity r = minRarity; r <= maxRarity; ++r) totalRarityWeight += rarityDistribution[r];
		return totalRarityWeight;
	}

	/// <summary>
	/// Chooses a random rarity from the rarityDistribution.
	/// </summary>
	/// <returns>The choosen rarity.</returns>
	/// <exception cref="System.Exception">Thrown if no rarity is choosen. Should never happen.</exception>
	public static ItemRarity GetRandomRarity(ItemRarity minRarity, ItemRarity maxRarity) {
		int rand = Game.random.Next() % ComputeTotalRarityWeight(minRarity, maxRarity);
		for (ItemRarity rarity = minRarity; rarity <= maxRarity; ++rarity) {
			rand -= rarityDistribution[rarity];
			if (rand < 0) return rarity;
		}
		throw new System.Exception("Unable to choose a rarity");
	}

	/// <summary>
	/// Get a random item, using the rarity distribution.
	/// </summary>
	/// <returns>A randomly choosen item from the pool.</returns>
	public static PassiveItem GetRandomItem(ItemRarity minRarity = ItemRarity.COMMON, ItemRarity maxRarity = ItemRarity.LEGENDARY) {
		List<PassiveItem> rarityPool = new List<PassiveItem>(items[GetRandomRarity(minRarity, maxRarity)].Values);
		return rarityPool[Game.random.Next() % rarityPool.Count];
	}
}
