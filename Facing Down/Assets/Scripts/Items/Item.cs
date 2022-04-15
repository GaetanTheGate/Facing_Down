using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item{
    protected static string Name;
    protected static int amount;

    public virtual void OnPickup() { }
    public virtual void OnRemove() { }
}
