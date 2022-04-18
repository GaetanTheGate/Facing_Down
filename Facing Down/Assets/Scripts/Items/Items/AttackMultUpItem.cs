using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMultUpItem : Item
{
	public AttackMultUpItem() {
		ID = "AttackMultUp";
	}

	private readonly float multiplierAdd = 0.1f;
	public override void OnPickup() {
		Game.player.stat.statEntity.atkMultipler += multiplierAdd;
		Game.player.stat.statEntity.computeAtk();
	}

	public override void OnRemove() {
		Game.player.stat.statEntity.atkMultipler -= multiplierAdd;
		Game.player.stat.statEntity.computeAtk();
	}
}
