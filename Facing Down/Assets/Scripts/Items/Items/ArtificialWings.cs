using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtificialWings : Item {
	public ArtificialWings() : base("ArtificialWings", ItemRarity.COMMON, ItemType.WIND) { }

	public override void OnPickup() {
		Game.player.stat.ModifyMaxDashes(1);
	}

	public override void OnRemove() {
		Game.player.stat.ModifyMaxDashes(-1);
	}

	public override Item MakeCopy() {
		return new ArtificialWings();
	}
}
