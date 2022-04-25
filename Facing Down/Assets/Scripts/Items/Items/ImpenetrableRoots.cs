using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpenetrableRoots : Item
{
	public ImpenetrableRoots() : base("ImpenetrableRoots", ItemRarity.COMMON, ItemType.EARTH) { }

	public override DammageInfo OnTakeDamage(DammageInfo damage) {
		EntityCollisionStructure playerEntity = Game.player.self.GetComponent<EntityCollisionStructure>();
		if (playerEntity.isGrounded || playerEntity.isWalled || playerEntity.isCeilinged) {
			damage.amount *= Mathf.Pow(0.9f, amount);
			return damage;			
		}
		return damage;
	}

	public override Item makeCopy() {
		return new ImpenetrableRoots();
	}
}
