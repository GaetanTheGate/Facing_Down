using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	private bool isActive = true;
	private Item item;
	private ItemChoice choice;

	public void Start() {
		if (item == null) throw new System.Exception("Item is not set, please use SpawnItemPedestal to spawn in-game items.");
	}

	public void SetItem(Item item) {
		this.item = item;
	}

	public void SetItemChoice(ItemChoice choice) {
		this.choice = choice;
	}

	/// <summary>
	/// On collision with the player, adds the pickup to the inventory
	/// </summary>
	/// <param name="collision"></param>
	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player") && isActive) {
			Game.player.inventory.AddItem(item);
			Disable();
			if (choice != null) choice.DisablePedestals();
		}
	}

	public void Disable() {
		if (!isActive) return;
		Debug.Log("ITEM DISABLED");
		isActive = false;
		Destroy(gameObject);
	}
}
