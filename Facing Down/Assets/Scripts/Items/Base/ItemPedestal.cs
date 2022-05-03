using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPedestal : MonoBehaviour
{
	public static readonly string itemSpritesPath = "Items/Sprites/";
	public static readonly string pedestalSpritesPath = "Items/Pedestal/";
	private static readonly Dictionary<ItemRarity, string> pedestalShapes;
	private static readonly Dictionary<ItemType, string> pedestalColors;

	private Item item;
	private bool isActive = false;

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
	/// Uses the ItemPool to spawn a random Item pickup
	/// </summary>
	/// <param name="parent">The pickup's tranform's parent</param>
	/// <param name="position">The pickup's position</param>
	/// <returns>The created pickup</returns>
	public static ItemPedestal SpawnRandomItemPickup(GameObject parent, Vector2 position) {
		ItemPedestal pickup = GameObject.Instantiate<ItemPedestal>(Resources.Load<ItemPedestal>("Prefabs/Items/ItemPedestal"));
		pickup.Init(ItemPool.GetRandomItem());
		pickup.transform.parent = parent.transform;
		pickup.transform.position = position;
		return pickup;
	}

	/// <summary>
	/// Sets a specific item for the pickup
	/// </summary>
	/// <param name="item"></param>
	public void Init(Item item) {
		this.item = item;
		Debug.Log(item.GetAmount());
		foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
			if (sr.name == "PedestalShape") sr.sprite = Resources.Load<Sprite>(pedestalSpritesPath + pedestalShapes[item.GetRarity()]);
			if (sr.name == "PedestalColor") sr.sprite = Resources.Load<Sprite>(pedestalSpritesPath + pedestalColors[item.GetItemType()]);
			if (sr.name == "ItemSprite") sr.sprite = Resources.Load<Sprite>(itemSpritesPath +item.GetID());
		}
		isActive = true;
	}

	/// <summary>
	/// On collision with the player, adds the pickup to the inventory
	/// </summary>
	/// <param name="collision"></param>
	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player") && isActive) {
			Game.player.inventory.AddItem(item);
			isActive = false;
			Destroy(gameObject);
		}
	}
}
