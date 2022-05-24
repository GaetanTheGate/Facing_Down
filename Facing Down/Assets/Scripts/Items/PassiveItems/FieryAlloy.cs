using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieryAlloy : PassiveItem
{	
	private readonly float atkAdd = 0.1f;

	public FieryAlloy() : base("FieryAlloy", ItemRarity.UNCOMMON, ItemType.FIRE) {}

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, atkAdd * 100);
	}

	public override void OnPickup() {
		Game.player.stat.ModifyAtk(atkAdd * Game.player.stat.BASE_ATK);
	}

	public override void OnRemove() {
		Game.player.stat.ModifyAtk( - atkAdd * Game.player.stat.BASE_ATK);
	}
}
