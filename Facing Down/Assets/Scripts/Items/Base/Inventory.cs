using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inventory manages multiple items for the player.
/// </summary>
public class Inventory : MonoBehaviour
{
	readonly Dictionary<string, Item> items;

	/// <summary>
	/// Initializes values.
	/// </summary>
	public Inventory() {
		items = new Dictionary<string, Item>();
	}

	/// <summary>
	/// Adds an item to the inventory.
	/// </summary>
	/// <param name="item">The item to add. The item's amount is taken into account.</param>
	public void AddItem(Item item) {
		if (!items.ContainsKey(item.getID())) {
			items.Add(item.getID(), item);
			UI.inventoryDisplay.AddItemDisplay(item);
		}
		else {
			items[item.getID()].modifyAmount(item.getAmount());
			UI.inventoryDisplay.UpdateItemDisplay(item);
		}
		for (int i = 0; i < item.getAmount(); ++i)
			item.OnPickup();
	}

	/// <summary>
	/// Removes 1 of the item from the inventory.
	/// </summary>
	/// <param name="item">The item to remove.</param>
	public void RemoveItem(Item item) {
		items[item.getID()].modifyAmount(-1);
		if (items[item.getID()].getAmount() == 0) {
			items.Remove(item.getID());
			UI.inventoryDisplay.RemoveItemDisplay(item);
		}
		else {
			UI.inventoryDisplay.UpdateItemDisplay(item);
		}
		item.OnRemove();
	}

	//Effect handlers

	public float OnTakeDamage(float damage) {
		List<Item> delayedItems = new List<Item>();
		foreach (Item item in items.Values) {
			if (item.getPriority() == ItemPriority.DELAYED) delayedItems.Add(item);
			else damage = item.OnTakeDamage(damage);
		}
		foreach (Item item in delayedItems) {
			damage = item.OnTakeDamage(damage);
		}
		return damage;
	} //TODO : retirer quand il sera entièrement remplacé par OnTakeDamage(DamageInfo)
	
	public DamageInfo OnTakeDamage(DamageInfo damage) {
		List<Item> delayedItems = new List<Item>();
		foreach (Item item in items.Values) {
			if (item.getPriority() == ItemPriority.DELAYED) delayedItems.Add(item);
			else damage = item.OnTakeDamage(damage);
		}
		foreach (Item item in delayedItems) {
			damage = item.OnTakeDamage(damage);
		}
		return damage;
	}
	
	public DamageInfo OnDealDamage(DamageInfo damage) {
		List<Item> delayedItems = new List<Item>();
		foreach (Item item in items.Values) {
			if (item.getPriority() == ItemPriority.DELAYED) delayedItems.Add(item);
			else damage = item.OnTakeDamage(damage);
		}
		foreach (Item item in delayedItems) {
			damage = item.OnTakeDamage(damage);
		}
		return damage;
	}

	/// <summary>
	/// Effect on death. Can prevent death.
	/// </summary>
	public void OnDeath() {
		List<Item> delayedItems = new List<Item>();
		foreach (Item item in items.Values) {
			if (item.getPriority() == ItemPriority.DELAYED) delayedItems.Add(item);
			else item.OnDeath();
		}
		foreach (Item item in delayedItems) {
			if (item.OnDeath()) break; ;
		}
	}

	public void OnEnemyKill() {
		foreach (Item item in items.Values) {
			item.OnEnemyKill();
		}
	}

	public void OnGroundCollisionEnter(Collision collision) {
		foreach (Item item in items.Values) {
			item.OnGroundCollisionEnter(collision);
		}
	}
	public void OnGroundCollisionLeave(Collision collision) {
		foreach (Item item in items.Values) {
			item.OnGroundCollisionLeave(collision);
		}
	}
	
	public void OnBullettimeActivate() {
		foreach (Item item in items.Values) {
			item.OnBullettimeActivate();
		}
	}

	public void OnRoomFinish() {
		foreach (Item item in items.Values) {
			item.OnRoomFinish();
		}
	}
	public void OnDash() {
		foreach (Item item in items.Values) {
			item.OnDash();
		}
	}
	public void OnRedirect() {
		foreach (Item item in items.Values) {
			item.OnMegaDash();
		}
	}
	public void OnMegaDash() {
		foreach (Item item in items.Values) {
			item.OnMegaDash();
		}
	}

	public Dictionary<string, Item> GetItems() {
		return items;
	}
}
