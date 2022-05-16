using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SylvanBreastplate : PassiveItem
{
    private readonly float damageReduction = 10f;
    public SylvanBreastplate() : base("SylvanBreastplate", ItemRarity.EPIC, ItemType.EARTH, ItemPriority.DELAYED) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, damageReduction);
	}
	public override DamageInfo OnTakeDamage(DamageInfo damage) {
		damage.amount = Mathf.Max(damage.amount - damageReduction * amount, 1);
		return damage;
	}
}
