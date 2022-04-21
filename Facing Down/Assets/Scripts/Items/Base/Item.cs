using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item{
    private string ID;
    protected int amount;
    private ItemRarity rarity;
    private ItemType type;

    public Item(string id, ItemRarity rarity, ItemType type) {
        this.ID = id;
        this.rarity = rarity;
        this.type = type;
        amount = 1;
	}

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

    public virtual void OnPickup() {}
    public virtual void OnRemove() {}

    public void modifyAmount(int modif) {
        amount += modif;
	}

    public abstract Item makeCopy();
}