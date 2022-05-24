using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightness : PassiveItem
{
    private readonly float accelerationIncrease = 0.1f;
    public Lightness() : base("Lightness", ItemRarity.UNCOMMON, ItemType.WIND) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, accelerationIncrease * 100);
	}

	public override void OnPickup() {
		Game.player.stat.ModifyAcceleration(accelerationIncrease * Game.player.stat.BASE_ACCELERATION);
	}

	public override void OnRemove() {
		Game.player.stat.ModifyAcceleration(-accelerationIncrease * Game.player.stat.BASE_ACCELERATION);
	}
}
