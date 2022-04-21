using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaticCoating : Item
{
	public MagmaticCoating() : base("MagmaticCoating", ItemRarity.COMMON, ItemType.FIRE) { }

	private readonly float critRate = 10;
	public override void OnPickup() {
		Game.player.stat.statEntity.critRate += critRate;
	}

	public override void OnRemove() {
		Game.player.stat.statEntity.critRate -= critRate;
	}

	public override Item makeCopy() {
		return new MagmaticCoating();
	}
}
