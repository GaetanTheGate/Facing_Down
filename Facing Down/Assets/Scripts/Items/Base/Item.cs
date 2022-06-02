using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class from which all the items inherit.
/// </summary>
public abstract class Item{

    private string ID;
    protected ItemDescription description;
    private Sprite sprite;

    public Item(string id) {
        ID = id;
        InitDescription(id);
        sprite = Resources.Load<Sprite>(GetSpriteFolderPath() + ID);
        if (sprite == null) sprite = Resources.Load<Sprite>("Sprites/Items/Items/PlaceHolder");
	}

    protected abstract void InitDescription(string id);

    //getters

    public string GetID() {
        return ID;
	}

    public string GetName() {
        return description.NAME;
	}

    public abstract string GetDescription();

    public Sprite GetSprite() {
        return sprite;
	}

    public abstract string GetSpriteFolderPath();

    //Effects

    public virtual void OnPickup() {}
    public virtual void OnRemove() {}

    /// <summary>
    /// Effect when damage is taken. May change the damage amount.
    /// </summary>
    /// <param name="damage">The amount of damage taken.</param>
    /// <returns>The new amount of damage that will be taken.</returns>
    public virtual DamageInfo OnTakeDamage(DamageInfo damage) { return damage; }

    public virtual DamageInfo OnDealDamage(DamageInfo damage) { return damage; }

    public virtual void OnAttack() {}

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

    /// <summary>
    /// 
    /// </summary>
    /// <returns>A copy of the item.</returns>
    public abstract Item MakeCopy();
}