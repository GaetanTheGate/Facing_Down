using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusAmber : PassiveItem
{
    private readonly float specialDurationIncrease = 0.4f;
    public FocusAmber() : base("FocusAmber", ItemRarity.COMMON, ItemType.THUNDER) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, specialDurationIncrease * 100);
	}

	public override void OnPickup() {
		Game.player.stat.ModifySpecialDuration(specialDurationIncrease);
	}

	public override void OnRemove() {
		Game.player.stat.ModifySpecialDuration(specialDurationIncrease);
	}
}
