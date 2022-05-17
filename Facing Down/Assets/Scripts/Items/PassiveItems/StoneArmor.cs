using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneArmor : PassiveItem
{
	public StoneArmor() : base("StoneArmor", ItemRarity.UNCOMMON, ItemType.EARTH) { }

	private readonly int maxHPAdd = 100;

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, maxHPAdd);
	}

	public override void OnPickup() {
		Game.player.stat.ModifyMaxHP(maxHPAdd);
		Game.player.stat.SetCurrentHP(Game.player.stat.GetCurrentHP() + maxHPAdd);
	}

	public override void OnRemove() {
		Game.player.stat.ModifyMaxHP(-maxHPAdd);
	}
}
