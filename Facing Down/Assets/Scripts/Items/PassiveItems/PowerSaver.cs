using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSaver : PassiveItem
{
    private readonly float specialRetrieved = 0.2f;
    public PowerSaver() : base("PowerSaver", ItemRarity.UNCOMMON, ItemType.THUNDER) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, specialRetrieved * 100);
	}

	public override void OnBullettimeEnd() {
		Game.player.stat.ModifySpecialLeft(1 - Mathf.Pow(1 - specialRetrieved, amount));
	}
}
