using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneArmor : Item
{
	public StoneArmor() : base("StoneArmor", ItemRarity.UNCOMMON, ItemType.EARTH) { }

	private readonly int maxHPAdd = 100;
	public override void OnPickup() {
		Game.player.stat.maxHitPoints += maxHPAdd;
		Game.player.stat.currentHitPoints += maxHPAdd;
	}

	public override void OnRemove() {
		Game.player.stat.maxHitPoints -= maxHPAdd;
	}

	public override Item MakeCopy() {
		return new StoneArmor();
	}
}
