using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SylvanBreastplate : Item
{
    private readonly float damageReduction = 10f;
    public SylvanBreastplate() : base("SylvanBreastplate", ItemRarity.EPIC, ItemType.EARTH, ItemPriority.DELAYED) { }

	public override DamageInfo OnTakeDamage(DamageInfo damage) {
		damage.amount = Mathf.Max(damage.amount - damageReduction * amount, 1);
		return damage;
	}

	public override Item MakeCopy() {
		return new SylvanBreastplate();
	}
}
