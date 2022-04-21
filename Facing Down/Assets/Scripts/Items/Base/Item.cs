using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item{
    private string ID;
    protected int amount;
    private ItemRarity rarity;
    private ItemType type;
    private ItemPriority priority;

    public Item(string id, ItemRarity rarity, ItemType type, ItemPriority priority) {
        this.ID = id;
        this.rarity = rarity;
        this.type = type;
        this.priority = priority;
        amount = 1;
	}

    public Item(string id, ItemRarity rarity, ItemType type) : this(id, rarity, type, ItemPriority.IMMEDIATE) { }

    public string getID() {
        return ID;
	}
    public int getAmount() {
        return amount;
	}
    public ItemRarity GetRarity() {
        return rarity;
	}
    public ItemType getType() {
        return type;
	}

    public ItemPriority getPriority() {
        return priority;
	}

    public virtual void OnPickup() {}
    public virtual void OnRemove() {}

    public virtual float OnTakeDamage(float damage) { return damage; }

    //Return true to stop going through the inventory
    public virtual bool OnDeath() { return false; }

    public void setAmount(int amount) {
        this.amount = amount;
	}

    public void modifyAmount(int modif) {
        amount += modif;
	}

    public abstract Item makeCopy();
}