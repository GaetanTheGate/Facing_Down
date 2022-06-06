using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSaver : PassiveItem
{
    private readonly float specialRetrieved = 0.2f;
	private float initialSpecialLeft;
    public PowerSaver() : base("PowerSaver", ItemRarity.UNCOMMON, ItemType.THUNDER) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, (1 - Mathf.Pow(1 - specialRetrieved, amount)) * 100);
	}

	public override void OnBullettimeActivate() {
		initialSpecialLeft = Game.player.stat.GetSpecialLeft();
	}

	public override void OnBullettimeEnd() {
		Debug.Log((1 - Mathf.Pow(1 - specialRetrieved, amount)));
		Game.player.stat.ModifySpecialLeft((initialSpecialLeft - Game.player.stat.GetSpecialLeft()) * (1 - Mathf.Pow(1 - specialRetrieved, amount)));
	}
}
