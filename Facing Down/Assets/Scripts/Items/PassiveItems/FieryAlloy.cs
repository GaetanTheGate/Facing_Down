using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieryAlloy : PassiveItem
{	
	private readonly float multiplierAdd = 0.1f;

	public FieryAlloy() : base("FieryAlloy", ItemRarity.UNCOMMON, ItemType.FIRE) {}

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, multiplierAdd * 100);
	}

	public override void OnPickup() {
		Game.player.stat.atkMultipler += multiplierAdd;
		Game.player.stat.computeAtk();
	}

	public override void OnRemove() {
		Game.player.stat.atkMultipler -= multiplierAdd;
		Game.player.stat.computeAtk();
	}
}
