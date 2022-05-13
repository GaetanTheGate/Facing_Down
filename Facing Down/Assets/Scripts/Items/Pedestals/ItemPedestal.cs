using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// An Item Pedestal, which can grant the player new items. Use SpawnItemPedestal or SpawnRandomItemPedestal to instantiate.
/// </summary>
public class ItemPedestal : MonoBehaviour {
	private static ItemPedestal prefab;
	private static Dictionary<ItemRarity, Color> haloColors;
	private static Dictionary<ItemType, GameObject> pedestalPrefabs;

	private GameObject halo;
	private ItemPickup pickup;
	private ItemPedestalPreviewArea previewArea;

	private ItemChoice choice;

	private static void InitStaticValues() {
		prefab = Resources.Load<ItemPedestal>("Prefabs/Items/Pedestal/ItemPedestal");
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
	public static ItemPedestal SpawnRandomItemPedestal(Transform parent, Vector2 position) {
		return SpawnItemPedestal(ItemPool.GetRandomItem(), parent, position);
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
		GameObject.Instantiate<GameObject>(pedestalPrefabs[item.GetItemType()], itemPedestal.transform);

		itemPedestal.halo = itemPedestal.transform.Find("RarityHalo").gameObject;
		itemPedestal.pickup = itemPedestal.transform.Find("ItemPickup").GetComponent<ItemPickup>();
		itemPedestal.previewArea = itemPedestal.transform.Find("PedestalPreviewArea").GetComponent<ItemPedestalPreviewArea>();

		itemPedestal.halo.GetComponent<SpriteRenderer>().color = haloColors[item.GetRarity()];
		itemPedestal.pickup.SetItem(item);
		itemPedestal.pickup.SetPedestal(itemPedestal);
		itemPedestal.previewArea.SetItem(item);
		return itemPedestal;
	}

	/// <summary>
	/// Registers this pedestal as a choice for the chosen ItemChoice
	/// </summary>
	/// <param name="choice"></param>
	public void SetItemChoice(ItemChoice choice) {
		this.choice = choice;
	}

	public void DisablePedestal() {
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
		if (choice != null) choice.DisablePedestals();
	}
}