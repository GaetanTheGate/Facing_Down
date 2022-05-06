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
		for (int i = 0; i < item.GetAmount(); ++i) {
			if (!items.ContainsKey(item.GetID())) {
				items.Add(item.GetID(), item.MakeCopy());
				UI.inventoryDisplay.AddItemDisplay(items[item.GetID()]);
			}
			else {
				items[item.GetID()].ModifyAmount(1);
			}
			items[item.GetID()].OnPickup();
		}
		UI.inventoryDisplay.UpdateItemDisplay(items[item.GetID()]);
	}

	/// <summary>
	/// Removes 1 of the item from the inventory.
	/// </summary>
	/// <param name="item">The item to remove.</param>
	/// <returns>True if the item has been removed, else returns false</returns>
	public bool RemoveItem(Item item) {
		if (!items.ContainsKey(item.GetID())) return false;
		items[item.GetID()].ModifyAmount(-1);
		items[item.GetID()].OnRemove();
		if (items[item.GetID()].GetAmount() == 0) {
			items.Remove(item.GetID());
			UI.inventoryDisplay.RemoveItemDisplay(item);
		}
		else {
			UI.inventoryDisplay.UpdateItemDisplay(items[item.GetID()]);
		}
		return true;
	}

	//Effect handlers

	public float OnTakeDamage(float damage) {
		List<Item> delayedItems = new List<Item>();
		foreach (Item item in items.Values) {
			if (item.GetPriority() == ItemPriority.DELAYED) delayedItems.Add(item);
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
			if (item.GetPriority() == ItemPriority.DELAYED) delayedItems.Add(item);
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
			if (item.GetPriority() == ItemPriority.DELAYED) delayedItems.Add(item);
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
			if (item.GetPriority() == ItemPriority.DELAYED) delayedItems.Add(item);
			else item.OnDeath();
		}
		foreach (Item item in delayedItems) {
			if (item.OnDeath()) break; ;
		}
	}

	public void OnEnemyKill(Entity enemy) {
		foreach (Item item in items.Values) {
			item.OnEnemyKill(enemy);
		}
	}

	public void OnGroundCollisionEnter() {
		foreach (Item item in items.Values) {
			item.OnGroundCollisionEnter();
		}
	}
	public void OnGroundCollisionLeave() {
		foreach (Item item in items.Values) {
			item.OnGroundCollisionLeave();
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
			item.OnRedirect();
		}
	}
	public void OnMegaDash() {
		foreach (Item item in items.Values) {
			item.OnMegaDash();
		}
	}

	public void OnBullettimeEnd() {
		foreach (Item item in items.Values) {
			item.OnBullettimeEnd();
		}
	}

	public Dictionary<string, Item> GetItems() {
		return items;
	}
}
