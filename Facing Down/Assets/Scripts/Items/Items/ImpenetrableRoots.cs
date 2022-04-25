using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpenetrableRoots : Item
{
	private float damageReduction = 0.9f;
	public ImpenetrableRoots() : base("ImpenetrableRoots", ItemRarity.COMMON, ItemType.EARTH) { }

	public override DamageInfo OnTakeDamage(DamageInfo damage) {
		EntityCollisionStructure playerEntity = Game.player.self.GetComponent<EntityCollisionStructure>();
		if (playerEntity.isGrounded || playerEntity.isWalled || playerEntity.isCeilinged) {
			damage.amount *= Mathf.Pow(damageReduction, amount);
			return damage;			
		}
		return damage;
	}

	public override Item makeCopy() {
		return new ImpenetrableRoots();
	}
}
