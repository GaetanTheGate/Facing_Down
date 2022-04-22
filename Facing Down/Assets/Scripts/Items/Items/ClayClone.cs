using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClayClone : Item {
	public ClayClone() : base("ClayClone", ItemRarity.LEGENDARY, ItemType.EARTH, ItemPriority.DELAYED) { }

	public override bool OnDeath() {
		Game.player.stat.currentHitPoints = Game.player.stat.maxHitPoints;
		Game.player.inventory.RemoveItem(this);
		return true;
	}

	public override Item makeCopy() {
		return new ClayClone();
	}
}
