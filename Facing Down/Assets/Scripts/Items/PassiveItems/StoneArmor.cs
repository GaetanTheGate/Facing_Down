using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneArmor : PassiveItem
{
	public StoneArmor() : base("StoneArmor", ItemRarity.UNCOMMON, ItemType.EARTH) { }

	private readonly float maxHPAdd = 0.1f;

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, maxHPAdd * amount * 100);
	}

	public override void OnPickup() {
		Game.player.stat.ModifyMaxHP(Mathf.FloorToInt(maxHPAdd * Game.player.stat.BASE_HP));
		Game.player.stat.SetCurrentHP(Game.player.stat.GetCurrentHP() + Mathf.FloorToInt(maxHPAdd * Game.player.stat.BASE_HP));
	}

	public override void OnRemove() {
		Game.player.stat.ModifyMaxHP(-Mathf.FloorToInt(maxHPAdd * Game.player.stat.BASE_HP));
	}
}
