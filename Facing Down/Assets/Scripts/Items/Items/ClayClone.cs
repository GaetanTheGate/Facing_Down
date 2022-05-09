using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClayClone : Item {
	public ClayClone() : base("ClayClone", ItemRarity.LEGENDARY, ItemType.EARTH, ItemPriority.DELAYED) { }

	public override string GetDescription() {
		return description.DESCRIPTION;
	}

	public override bool OnDeath() {
		Game.player.stat.SetCurrentHP(Game.player.stat.GetMaxHP());
		Game.player.inventory.RemoveItem(this);
		return true;
	}

	public override Item MakeCopy() {
		return new ClayClone();
	}
}
