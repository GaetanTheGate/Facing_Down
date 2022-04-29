using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSaver : Item
{
    private readonly float powerRetreived = 0.2f;
    public PowerSaver() : base("PowerSaver", ItemRarity.UNCOMMON, ItemType.THUNDER) { }

	public override void OnBullettimeEnd() {
		Game.player.stat.specialLeft += 1 - Mathf.Pow((1 - powerRetreived), amount);
	}

	public override Item MakeCopy() {
		return new PowerSaver();
	}
}
