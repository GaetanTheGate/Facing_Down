using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
	public static readonly string spriteFolderPath = "Items/Sprites/";
	private Item item;
	private bool isActive = false;

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
