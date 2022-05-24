using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpenetrableRoots : PassiveItem
{
	private float damageReduction = 0.9f;
	public ImpenetrableRoots() : base("ImpenetrableRoots", ItemRarity.COMMON, ItemType.EARTH) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, (1 - Mathf.Pow(damageReduction, amount)) * 100);
	}

	public override DamageInfo OnTakeDamage(DamageInfo damage) {
		EntityCollisionStructure playerEntity = Game.player.self.GetComponent<EntityCollisionStructure>();
		if (playerEntity.isGrounded || playerEntity.isWalled || playerEntity.isCeilinged) {
			damage.amount *= Mathf.Pow(damageReduction, amount);
			return damage;			
		}
		return damage;
	}
}
