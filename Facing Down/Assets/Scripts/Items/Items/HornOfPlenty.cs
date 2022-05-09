using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornOfPlenty : Item
{
	private float healAmount = 0.05f;

    public HornOfPlenty() : base("HornOfPlenty", ItemRarity.RARE, ItemType.EARTH) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, healAmount * 100);
	}

	public override void OnRoomFinish() {
		Game.player.stat.Heal(amount * healAmount * Game.player.stat.GetMaxHP());
	}

	public override Item MakeCopy() {
		return new HornOfPlenty();
	}
}
