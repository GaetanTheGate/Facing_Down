using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialWings : PassiveItem {
	public ArtificialWings() : base("ArtificialWings", ItemRarity.COMMON, ItemType.WIND) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, 1);
	}

	public override void OnPickup() {
		Game.player.stat.ModifyMaxDashes(1);
	}

	public override void OnRemove() {
		Game.player.stat.ModifyMaxDashes(-1);
	}
}
