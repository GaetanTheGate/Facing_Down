using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneArmor : Item
{
	public StoneArmor() : base("StoneArmor", ItemRarity.UNCOMMON, ItemType.EARTH) { }

	private readonly int maxHPAdd = 1;
	public override void OnPickup() {
		Game.player.stat.statEntity.maxHitPoints += maxHPAdd;
		Game.player.stat.statEntity.currentHitPoints += maxHPAdd;
	}

	public override void OnRemove() {
		Game.player.stat.statEntity.maxHitPoints -= maxHPAdd;
	}

	public override Item makeCopy() {
		return new StoneArmor();
	}
}
