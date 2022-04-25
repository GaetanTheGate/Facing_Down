using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornOfPlenty : Item
{
	private float healAmount = 0.05f;

    public HornOfPlenty() : base("HornOfPlenty", ItemRarity.RARE, ItemType.EARTH) { }

	public override void OnRoomFinish() {
		Game.player.stat.Heal(amount * healAmount * Game.player.stat.maxHitPoints);
	}

	public override Item makeCopy() {
		return new HornOfPlenty();
	}
}
