using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public static readonly string spriteFolderPath = "Items/";
	private Item item;

	public void Init(Item item) {
		this.item = item;
		GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(spriteFolderPath + item.GetID());
	}

	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			Game.player.inventory.AddItem(item);
			Destroy(gameObject);
		}
	}
}
