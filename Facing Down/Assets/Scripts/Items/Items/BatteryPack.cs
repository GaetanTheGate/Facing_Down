using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPack : Item
{
	private readonly int maxSpecialIncrease = 1;
    public BatteryPack() : base("BatteryPack", ItemRarity.EPIC, ItemType.THUNDER) { }

	public override void OnPickup() {
		Game.player.stat.ModifyMaxSpecial(maxSpecialIncrease);
	}

	public override void OnRemove() {
		Game.player.stat.ModifyMaxSpecial(-maxSpecialIncrease);
	}

	public override Item MakeCopy() {
		return new BatteryPack();
	}
}
