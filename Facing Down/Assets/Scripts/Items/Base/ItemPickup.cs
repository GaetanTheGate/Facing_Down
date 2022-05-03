using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	private bool isActive = true;
	private Item item;

	public void Start() {
		if (item == null) throw new System.Exception("Item is not set, please use SpawnItemPedestal to spawn in-game items.");
	}

	public void SetItem(Item item) {
		this.item = item;
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
