using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Inventory manages multiple items for the player.
/// </summary>
public class Inventory
{
	private readonly Dictionary<string, PassiveItem> items;
	private Weapon weapon;

	/// <summary>
	/// Initializes values.
	/// </summary>
	public Inventory() {
		weapon = new StunShot("Enemy");
		items = new Dictionary<string, PassiveItem>();
	}

	/// <summary>
	/// Adds an item to the inventory.
	/// </summary>
	/// <param name="item">The item to add. The item's amount is taken into account.</param>
	public void AddItem(PassiveItem item) {
		for (int i = 0; i < item.GetAmount(); ++i) {
			if (!items.ContainsKey(item.GetID())) {
				items.Add(item.GetID(), (PassiveItem) item.MakeCopy());
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
	/// Sets the player's weapon.
	/// </summary>
	/// <param name="weapon"></param>
	
	//TODO : give bonuses when changing weapon
	public void SetWeapon (Weapon weapon) {
		Weapon previousWeapon = this.weapon;
		previousWeapon.OnRemove();
		this.weapon = weapon;
		Game.player.stat.SetCurrentHP(Mathf.CeilToInt(Game.player.stat.GetCurrentHP() * weapon.stat.HPMult / previousWeapon.stat.HPMult));
		this.weapon.OnPickup();
		Game.player.stat.ResetSpecial();
		UI.healthBar.UpdateHP();
		UI.dashBar.UpdateDashes();
	}

	public Weapon GetWeapon() => weapon;

	/// <summary>
	/// Removes 1 of the item from the inventory.
	/// </summary>
	/// <param name="item">The item to remove.</param>
	/// <returns>True if the item has been removed, else returns false</returns>
	public bool RemoveItem(PassiveItem item) {
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
	
	public DamageInfo OnTakeDamage(DamageInfo damage) {
		damage = weapon.OnTakeDamage(damage);
		List<PassiveItem> delayedItems = new List<PassiveItem>();
		foreach (PassiveItem item in items.Values) {
			if (item.GetPriority() == ItemPriority.DELAYED) delayedItems.Add(item);
			else damage = item.OnTakeDamage(damage);
		}
		foreach (PassiveItem item in delayedItems) {
			damage = item.OnTakeDamage(damage);
		}
		return damage;
	}
	
	public DamageInfo OnDealDamage(DamageInfo damage) {
		weapon.OnDealDamage(damage);
		List<PassiveItem> delayedItems = new List<PassiveItem>();
		foreach (PassiveItem item in items.Values) {
			if (item.GetPriority() == ItemPriority.DELAYED) delayedItems.Add(item);
			else damage = item.OnDealDamage(damage);
		}
		foreach (PassiveItem item in delayedItems) {
			damage = item.OnDealDamage(damage);
		}
		return damage;
	}

	public void BeforeAttack() {
		weapon.BeforeAttack();
		foreach(PassiveItem item in items.Values) {
			item.BeforeAttack();
		}
	}

	/// <summary>
	/// Effect on death. Can prevent death.
	/// </summary>
	public void OnDeath() {
		if (weapon.OnDeath()) return;
		List<PassiveItem> delayedItems = new List<PassiveItem>();
		foreach (PassiveItem item in items.Values) {
			if (item.GetPriority() == ItemPriority.DELAYED) delayedItems.Add(item);
			else item.OnDeath();
		}
		foreach (PassiveItem item in delayedItems) {
			if (item.OnDeath()) break; ;
		}
	}

	public void OnEnemyKill(Entity enemy) {
		weapon.OnEnemyKill(enemy);
		foreach (PassiveItem item in items.Values) {
			item.OnEnemyKill(enemy);
		}
	}

	public void OnGroundCollisionEnter() {
		weapon.OnGroundCollisionEnter();
		foreach (PassiveItem item in items.Values) {
			item.OnGroundCollisionEnter();
		}
	}
	public void OnGroundCollisionLeave() {
		weapon.OnGroundCollisionLeave();
		foreach (PassiveItem item in items.Values) {
			item.OnGroundCollisionLeave();
		}
	}
	
	public void OnBullettimeActivate() {
		weapon.OnBullettimeActivate();
		foreach (PassiveItem item in items.Values) {
			item.OnBullettimeActivate();
		}
	}

	public void OnRoomFinish() {
		weapon.OnRoomFinish();
		foreach (PassiveItem item in items.Values) {
			item.OnRoomFinish();
		}
	}
	public void OnDash() {
		weapon.OnDash();
		foreach (PassiveItem item in items.Values) {
			item.OnDash();
		}
	}
	public void OnRedirect() {
		weapon.OnRedirect();
		foreach (PassiveItem item in items.Values) {
			item.OnRedirect();
		}
	}
	public void OnMegaDash() {
		weapon.OnMegaDash();
		foreach (PassiveItem item in items.Values) {
			item.OnMegaDash();
		}
	}

	public void OnBullettimeEnd() {
		weapon.OnBullettimeEnd();
		foreach (PassiveItem item in items.Values) {
			item.OnBullettimeEnd();
		}
	}

	public Dictionary<string, PassiveItem> GetItems() {
		return items;
	}
}
