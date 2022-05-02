using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class from which all the items inherit.
/// </summary>
public abstract class Item{
    private string ID;
    protected int amount;
    private ItemRarity rarity;
    private ItemType type;
    private ItemPriority priority;

    /// <summary>
    /// Item constructor. Sets the amount to 1 by default.
    /// </summary>
    /// <param name="id">Item's ID.</param>
    /// <param name="rarity">Item's rarity</param>
    /// <param name="type">Item's type</param>
    /// <param name="priority">Item's priority when used.</param>
    public Item(string id, ItemRarity rarity, ItemType type, ItemPriority priority) {
        this.ID = id;
        this.rarity = rarity;
        this.type = type;
        this.priority = priority;
        amount = 1;
	}

    /// <summary>
    /// Item constructor. Sets the amount to 1 and the priority to immediate.
    /// </summary>
    /// <param name="id">Item's ID.</param>
    /// <param name="rarity">Item's rarity</param>
    /// <param name="type">Item's type</param>
    public Item(string id, ItemRarity rarity, ItemType type) : this(id, rarity, type, ItemPriority.IMMEDIATE) { }

    //getters

    public string GetID() {
        return ID;
	}
    public int GetAmount() {
        return amount;
	}
    public ItemRarity GetRarity() {
        return rarity;
	}
    public ItemType GetItemType() {
        return type;
	}

    public ItemPriority GetPriority() {
        return priority;
	}

    //Effects

    public virtual void OnPickup() {}
    public virtual void OnRemove() {}

    /// <summary>
    /// Effect when damage is taken. May change the damage amount.
    /// </summary>
    /// <param name="damage">The amount of damage taken.</param>
    /// <returns>The new amount of damage that will be taken.</returns>
    public virtual float OnTakeDamage(float damage) { return damage; } //TODO : Retirer quand il sera entièrement remplacé par OnTakeDamage(DamageInfo)
    public virtual DamageInfo OnTakeDamage(DamageInfo damage) { return damage; }

    public virtual DamageInfo OnDealDamage(DamageInfo damage) { return damage; }

    /// <summary>
    /// Effect when the player dies.
    /// </summary>
    /// <returns>True if the death is prevented. WARNING : Death preventing effects should be only on delayed items</returns>
    public virtual bool OnDeath() { return false; }

    public virtual void OnEnemyKill(Entity enemy) {}

    public virtual void OnGroundCollisionEnter() {}

    public virtual void OnGroundCollisionLeave() {}

    public virtual void OnBullettimeActivate() {}

    public virtual void OnRoomFinish() {}

    public virtual void OnDash() {}

    public virtual void OnRedirect() {}

    public virtual void OnMegaDash() {}

    public virtual void OnBullettimeEnd() { }

    public void SetAmount(int amount) {
        this.amount = amount;
	}

    public void ModifyAmount(int modif) {
        amount += modif;
	}

    /// <summary>
    /// 
    /// </summary>
    /// <returns>A copy of the item.</returns>
    public abstract Item MakeCopy();
}