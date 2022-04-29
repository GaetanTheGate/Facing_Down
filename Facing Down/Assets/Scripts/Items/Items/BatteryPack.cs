using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPack : Item
{
	private readonly int maxEnergyIncrease = 1;
    public BatteryPack() : base("BatteryPack", ItemRarity.EPIC, ItemType.THUNDER) { }

	public override void OnPickup() {
		Game.player.stat.maxSpecial += maxEnergyIncrease;
	}

	public override void OnRemove() {
		Game.player.stat.maxSpecial -= maxEnergyIncrease;
	}

	public override Item MakeCopy() {
		return new BatteryPack();
	}
}
