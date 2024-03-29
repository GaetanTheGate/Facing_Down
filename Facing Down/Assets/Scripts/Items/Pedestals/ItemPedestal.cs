using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// An Item Pedestal, which can grant the player new items. Use SpawnItemPedestal or SpawnRandomItemPedestal to instantiate.
/// </summary>
public class ItemPedestal : Pedestal {
	private static ItemPedestal prefab;
	private static Dictionary<ItemRarity, Color> haloColors;
	private static Dictionary<ItemType, GameObject> pedestalPrefabs;
	private static GameObject weaponPrefab;

	private GameObject halo;
	private ItemPickup pickup;
	private ItemPedestalPreviewArea previewArea;

	private static void InitStaticValues() {
		prefab = Resources.Load<ItemPedestal>("Prefabs/Items/Pedestal/ItemPedestal");
		weaponPrefab = Resources.Load<GameObject>("Prefabs/Items/Pedestal/WeaponPedestal");
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
	public static ItemPedestal SpawnRandomItemPedestal(Transform parent, Vector2 position, ItemRarity minRarity, ItemRarity maxRarity) {
		return SpawnItemPedestal(ItemPool.GetRandomItem(minRarity, maxRarity), parent, position);
	}

	public static ItemPedestal SpawnRandomWeaponPedestal(Transform parent, Vector2 position) {
		return SpawnItemPedestal(WeaponPool.GetRandomWeapon(), parent, position);
	}

	/// <summary>
	/// Spawns an item pedestal
	/// </summary>
	/// <param name="item">The item to spawn</param>
	/// <param name="parent">The pickup's tranform's parent</param>
	/// <param name="position">The pickup's position</param>
	/// <returns>The created pedestal</returns>
	public static ItemPedestal SpawnItemPedestal(Item item, Transform parent, Vector2 position) {
		if (prefab == null) InitStaticValues();
		ItemPedestal itemPedestal = GameObject.Instantiate<ItemPedestal>(prefab);
		itemPedestal.transform.SetParent(parent);
		itemPedestal.transform.position = position;
		if (item is PassiveItem) {
			GameObject.Instantiate<GameObject>(pedestalPrefabs[((PassiveItem)item).GetItemType()], itemPedestal.transform);
		}
		else if (item is Weapon) {
			GameObject.Instantiate<GameObject>(weaponPrefab, itemPedestal.transform);
		}

		itemPedestal.halo = itemPedestal.transform.Find("RarityHalo").gameObject;
		itemPedestal.pickup = itemPedestal.transform.Find("ItemPickup").GetComponent<ItemPickup>();
		itemPedestal.previewArea = itemPedestal.transform.Find("PedestalPreviewArea").GetComponent<ItemPedestalPreviewArea>();

		itemPedestal.halo.GetComponent<SpriteRenderer>().color = haloColors[item is PassiveItem ? ((PassiveItem)item).GetRarity() : ItemRarity.LEGENDARY];
		itemPedestal.pickup.SetItem(item);
		itemPedestal.pickup.SetPedestal(itemPedestal);
		itemPedestal.previewArea.SetItem(item);
		return itemPedestal;
	}

	/// <summary>
	/// Disables this pedestal and the ones linked by an itemChoice.
	/// </summary>
	public override void DisablePedestal() {
		base.DisablePedestal();
		if (pickup != null) {
			pickup.Disable();
			pickup = null;
		}
		if (previewArea != null) {
			Destroy(previewArea);
			previewArea = null;
		}
		if (halo != null) {
			Destroy(halo);
			halo = null;
		}
	}
}