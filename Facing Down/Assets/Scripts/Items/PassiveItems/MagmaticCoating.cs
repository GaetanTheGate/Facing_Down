using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaticCoating : PassiveItem
{	
	private readonly float critRate = 10;
	public MagmaticCoating() : base("MagmaticCoating", ItemRarity.COMMON, ItemType.FIRE) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, critRate * amount);
	}

	public override void OnPickup() {
		Game.player.stat.critRate += critRate;
	}

	public override void OnRemove() {
		Game.player.stat.critRate -= critRate;
	}
}
