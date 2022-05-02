using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public static readonly string spriteFolderPath = "Items/Sprites/";
	private Item item;
	private bool isActive = false;

	public static ItemPickup SpawnRandomItemPickup(GameObject parent, Vector2 position) {
		ItemPickup pickup = GameObject.Instantiate<ItemPickup>(Resources.Load<ItemPickup>("Prefabs/Items/ItemPickup"));
		pickup.Init(ItemPool.GetRandomItem());
		pickup.transform.parent = parent.transform;
		pickup.transform.position = position;
		return pickup;
	}

	public void Init(Item item) {
		this.item = item;
		Debug.Log(item.GetAmount());
		GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteFolderPath + item.GetID());
		isActive = true;
	}

	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player") && isActive) {
			Game.player.inventory.AddItem(item);
			isActive = false;
			Destroy(gameObject);
		}
	}
}
