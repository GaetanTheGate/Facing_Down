using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartOfTheStorm : PassiveItem
{
    private readonly float cooldownReduction = 0.3f;
    private readonly float stackCooldownReduction = 0.1f;
    public HeartOfTheStorm() : base("HeartOfTheStorm", ItemRarity.UNCOMMON, ItemType.THUNDER) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, cooldownReduction * 100);
	}

	public override void OnEnemyKill(Entity enemy) {
		Game.player.stat.ReloadSpecial(cooldownReduction + (amount - 1) * stackCooldownReduction);
	}
}
