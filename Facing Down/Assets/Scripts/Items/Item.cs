using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item{
    protected static string ID;
    protected int amount = 0;

    public string getID() {
        return ID;
	}
    public int getAmount() {
        return amount;
	}

    public virtual void OnPickup() {}
    public virtual void OnRemove() {}

    public void modifyAmount(int modif) {
        amount += modif;
	}
}
