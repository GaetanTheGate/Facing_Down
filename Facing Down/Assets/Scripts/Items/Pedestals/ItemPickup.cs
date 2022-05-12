using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemPickup : MonoBehaviour {
	private bool isActive = true;
	private Item item;
	private ItemPedestal pedestal;

	public void Start() {
		if (item == null || pedestal == null) throw new System.Exception("ItemPickup has null attributes, please use SpawnItemPedestal to spawn in-game items.");
	}
	public void SetItem(Item item) {
		this.item = item;
	}

	public void SetPedestal(ItemPedestal pedestal) {
		this.pedestal = pedestal;
	}

	/// <summary>
	/// On collision with the player, adds the pickup to the inventory
	/// </summary>
	/// <param name="collision"></param>
	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player") && isActive) {
			Game.player.inventory.AddItem(item);
			pedestal.DisablePedestal();
		}
	}

	public void Disable() {
		if (!isActive) return;
		isActive = false;
		Destroy(gameObject);
	}
}