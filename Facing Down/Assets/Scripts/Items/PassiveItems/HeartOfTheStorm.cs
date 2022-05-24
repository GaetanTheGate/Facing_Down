using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartOfTheStorm : PassiveItem
{
    private readonly float cooldownReduction = 0.25f;
    public HeartOfTheStorm() : base("HeartOfTheStorm", ItemRarity.UNCOMMON, ItemType.THUNDER) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, cooldownReduction* amount * 100);
	}

	public override void OnEnemyKill(Entity enemy) {
		Game.player.stat.ModifySpecialLeft(cooldownReduction * amount * 100);
	}
}
