using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An Item Pedestal, which can grant the player new items. Use SpawnItemPedestal or SpawnRandomItemPedestal to instantiate.
/// </summary>
public class ItemPedestal : MonoBehaviour
{
	public static readonly string itemSpritesPath = "Items/Sprites/";
	public static readonly string pedestalSpritesPath = "Items/Pedestal/";
	private static readonly Dictionary<ItemRarity, string> pedestalShapes;
	private static readonly Dictionary<ItemType, string> pedestalColors;

	static ItemPedestal() {
		pedestalColors = new Dictionary<ItemType, string> {
			{ItemType.FIRE, "Color/PedestalFire"},
			{ItemType.EARTH, "Color/PedestalEarth"},
			{ItemType.THUNDER, "Color/PedestalThunder"},
			{ItemType.WIND, "Color/PedestalWind"}
		};
		pedestalShapes = new Dictionary<ItemRarity, string> {
			{ItemRarity.COMMON, "Shape/PedestalCommon"},
			{ItemRarity.UNCOMMON, "Shape/PedestalUncommon"},
			{ItemRarity.RARE, "Shape/PedestalRare"},
			{ItemRarity.EPIC, "Shape/PedestalEpic"},
			{ItemRarity.LEGENDARY, "Shape/PedestalLegendary"}
		};
	}

	/// <summary>
	/// Uses the ItemPool to spawn a random Item pedestal
	/// </summary>
	/// <param name="parent">The pickup's tranform's parent</param>
	/// <param name="position">The pickup's position</param>
	/// <returns>The created pedestal</returns>
	public static ItemPedestal SpawnRandomItemPedestal(GameObject parent, Vector2 position) {
		ItemPedestal pickup = GameObject.Instantiate<ItemPedestal>(Resources.Load<ItemPedestal>("Prefabs/Items/ItemPedestal"));
		pickup.Init(ItemPool.GetRandomItem());
		pickup.transform.parent = parent.transform;
		pickup.transform.position = position;
		return pickup;
	}

	/// <summary>
	/// Spawns an item pedestal
	/// </summary>
	/// <param name="item">The item to spawn</param>
	/// <param name="parent">The pickup's tranform's parent</param>
	/// <param name="position">The pickup's position</param>
	/// <returns>The created pedestal</returns>

	public static ItemPedestal SpawnItemPedestal(Item item, GameObject parent, Vector2 position) {
		ItemPedestal pickup = GameObject.Instantiate<ItemPedestal>(Resources.Load<ItemPedestal>("Prefabs/Items/ItemPedestal"));
		pickup.Init(item);
		pickup.transform.SetParent(parent.transform);
		pickup.transform.position = position;
		return pickup;
	}

	/// <summary>
	/// Sets a specific item for the pickup
	/// </summary>
	/// <param name="item"></param>
	private void Init(Item item) {
		foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
			if (sr.name == "PedestalShape") sr.sprite = Resources.Load<Sprite>(pedestalSpritesPath + pedestalShapes[item.GetRarity()]);
			if (sr.name == "PedestalColor") sr.sprite = Resources.Load<Sprite>(pedestalSpritesPath + pedestalColors[item.GetItemType()]);
			if (sr.name == "Item") sr.sprite = Resources.Load<Sprite>(itemSpritesPath +item.GetID());
		}
		GetComponentInChildren<ItemPickup>().SetItem(item);
	}

	public void SetItemChoice (ItemChoice choice) {
		GetComponentInChildren<ItemPickup>().SetItemChoice(choice);
	}

	public void DisablePedestal() {
		ItemPickup pickup = GetComponentInChildren<ItemPickup>();
		if (pickup != null) {
			pickup.Disable();
		}
	}
}
