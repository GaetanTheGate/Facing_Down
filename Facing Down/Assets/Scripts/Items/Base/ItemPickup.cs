using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public static readonly string spriteFolderPath = "Items/Sprites/";
	private Item item;
	private bool isActive = false;

	/// <summary>
	/// Uses the ItemPool to spawn a random Item pickup
	/// </summary>
	/// <param name="parent">The pickup's tranform's parent</param>
	/// <param name="position">The pickup's position</param>
	/// <returns>The created pickup</returns>
	public static ItemPickup SpawnRandomItemPickup(GameObject parent, Vector2 position) {
		ItemPickup pickup = GameObject.Instantiate<ItemPickup>(Resources.Load<ItemPickup>("Prefabs/Items/ItemPickup"));
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
		GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteFolderPath + item.GetID());
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
