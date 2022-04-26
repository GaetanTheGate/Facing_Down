using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartOfTheStorm : Item
{
    private readonly float cooldownReduction = 0.3f;
    private readonly float stackCooldownReduction = 0.1f;
    public HeartOfTheStorm() : base("HeartOfTheStorm", ItemRarity.UNCOMMON, ItemType.THUNDER) { }

	public override void OnEnemyKill() {
		Game.player.stat.ReloadSpecial(cooldownReduction + (amount - 1) * stackCooldownReduction);
	}

	public override Item MakeCopy() {
		return new HeartOfTheStorm();
	}
}
