using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusAmber : Item
{
    private readonly float specialDurationIncrease = 0.4f;
    public FocusAmber() : base("FocusAmber", ItemRarity.COMMON, ItemType.THUNDER) { }

	public override void OnPickup() {
		Game.player.stat.specialDuration += specialDurationIncrease;
	}

	public override void OnRemove() {
		Game.player.stat.specialDuration -= specialDurationIncrease;
	}

	public override Item MakeCopy() {
		return new FocusAmber();
	}
}
