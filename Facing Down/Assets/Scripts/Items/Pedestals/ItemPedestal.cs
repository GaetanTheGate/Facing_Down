using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// An Item Pedestal, which can grant the player new items. Use SpawnItemPedestal or SpawnRandomItemPedestal to instantiate.
/// </summary>
public class ItemPedestal : MonoBehaviour {
	public static readonly string itemSpritesPath = "Items/Sprites/";
	public static readonly string pedestalSpritesPath = "Items/Pedestal/";
	private static readonly GameObject rarityHaloPrefab = Resources.Load<GameObject>("Prefabs/Items/Pedestal/RarityHalo");
	private static readonly ItemPickup itemPickupPrefab = Resources.Load<ItemPickup>("Prefabs/Items/Pedestal/ItemPickup");
	private static readonly Dictionary<ItemRarity, Color> haloColors;
	private static readonly Dictionary<ItemType, GameObject> pedestalPrefabs;
	static ItemPedestal() {
		pedestalPrefabs = new Dictionary<ItemType, GameObject> {
			{ItemType.FIRE, Resources.Load<GameObject>("Prefabs/Items/Pedestal/FirePedestal")},
			{ItemType.EARTH, Resources.Load<GameObject>("Prefabs/Items/Pedestal/EarthPedestal")},
			{ItemType.THUNDER, Resources.Load<GameObject>("Prefabs/Items/Pedestal/ThunderPedestal")},
			{ItemType.WIND, Resources.Load<GameObject>("Prefabs/Items/Pedestal/WindPedestal")}
		};
		haloColors = new Dictionary<ItemRarity, Color> {
			{ItemRarity.COMMON, new Color(1, 1, 1)},
			{ItemRarity.UNCOMMON, new Color(0.5f, 0.78f, 1)},
			{ItemRarity.RARE, new Color(0.86f, 0.5f, 1)},
			{ItemRarity.EPIC, new Color(1, 0.86f, 0.25f)},
			{ItemRarity.LEGENDARY, new Color(1, 0.5f, 0.5f)}
		};
	}
	/// <summary>
	/// Uses the ItemPool to spawn a random Item pedestal
	/// </summary>
	/// <param name="parent">The pickup's tranform's parent</param>
	/// <param name="position">The pickup's position</param>
	/// <returns>The created pedestal</returns>
	public static ItemPedestal SpawnRandomItemPedestal(GameObject parent, Vector2 position) {
		return SpawnItemPedestal(ItemPool.GetRandomItem(), parent, position);
	}
	/// <summary>
	/// Spawns an item pedestal
	/// </summary>
	/// <param name="item">The item to spawn</param>
	/// <param name="parent">The pickup's tranform's parent</param>
	/// <param name="position">The pickup's position</param>
	/// <returns>The created pedestal</returns>
	public static ItemPedestal SpawnItemPedestal(Item item, GameObject parent, Vector2 position) {
		GameObject pedestal = GameObject.Instantiate<GameObject>(pedestalPrefabs[item.GetItemType()]);
		pedestal.AddComponent<ItemPedestal>();
		GameObject halo = GameObject.Instantiate<GameObject>(rarityHaloPrefab, pedestal.transform);
		halo.GetComponent<SpriteRenderer>().color = haloColors[item.GetRarity()];
		ItemPickup pickup = GameObject.Instantiate<ItemPickup>(itemPickupPrefab, pedestal.transform);
		pickup.transform.localPosition = new Vector2(0, 1);
		pickup.SetItem(item);
		pedestal.transform.SetParent(parent.transform);
		pedestal.transform.position = position;
		return pedestal.GetComponent<ItemPedestal>();
	}

	public void SetItemChoice(ItemChoice choice) {
		GetComponentInChildren<ItemPickup>().SetItemChoice(choice);
	}

	public void DisablePedestal() {
		ItemPickup pickup = GetComponentInChildren<ItemPickup>();
		if (pickup != null) {
			pickup.Disable();
		}
	}
}