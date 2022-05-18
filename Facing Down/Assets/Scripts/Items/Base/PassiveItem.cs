using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class from which all the passive items inherit.
/// </summary>
public abstract class PassiveItem : Item
{
    protected int amount;
    private ItemRarity rarity;
    private ItemType type;
    private ItemPriority priority;

    /// <summary>
    /// Passive Item constructor. Sets the amount to 1 by default.
    /// </summary>
    /// <param name="id">Item's ID.</param>
    /// <param name="rarity">Item's rarity</param>
    /// <param name="type">Item's type</param>
    /// <param name="priority">Item's priority when used.</param>
    public PassiveItem(string id, ItemRarity rarity, ItemType type, ItemPriority priority) : base(id)
    {
        this.rarity = rarity;
        this.type = type;
        this.priority = priority;
        amount = 1;
    }


    /// <summary>
    /// Passive Item constructor. Sets the amount to 1 and the priority to immediate.
    /// </summary>
    /// <param name="id">Item's ID.</param>
    /// <param name="rarity">Item's rarity</param>
    /// <param name="type">Item's type</param>
    public PassiveItem(string id, ItemRarity rarity, ItemType type) : this(id, rarity, type, ItemPriority.IMMEDIATE) { }

    public override string GetSpriteFolderPath()
    {
        return "Sprites/Items/Items/";
    }

	protected override void InitDescription(string id) {
        this.description = Localization.GetItemDescription(id);
    }

	public int GetAmount()
    {
        return amount;
    }
    public ItemRarity GetRarity()
    {
        return rarity;
    }
    public ItemType GetItemType()
    {
        return type;
    }

    public ItemPriority GetPriority()
    {
        return priority;
    }


    public void SetAmount(int amount)
    {
        this.amount = amount;
    }

    public void ModifyAmount(int modif)
    {
        amount += modif;
    }

    public override Item MakeCopy()
    {
        System.Type type = GetType();

        return (Item) type.GetConstructor(new System.Type[0]).Invoke(new Object[0]);
    }
}
