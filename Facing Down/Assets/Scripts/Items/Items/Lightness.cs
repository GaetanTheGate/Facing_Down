using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightness : Item
{
    private readonly float accelerationIncrease = 1f;
    public Lightness() : base("Lightness", ItemRarity.UNCOMMON, ItemType.WIND) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, accelerationIncrease * 10);
	}

	public override void OnPickup() {
		Game.player.stat.ModifyAcceleration(accelerationIncrease);
	}

	public override void OnRemove() {
		Game.player.stat.ModifyAcceleration(-accelerationIncrease);
	}

	public override Item MakeCopy() {
		return new Lightness();
	}
}
