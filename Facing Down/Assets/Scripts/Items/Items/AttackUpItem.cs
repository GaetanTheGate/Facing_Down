using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUpItem : Item
{
	public AttackUpItem() {
		ID = "AttackUp";
	}

	private int attackBuff = 10;
	public override void OnPickup() {
		Game.player.stat.baseAtk += attackBuff;
		Game.player.stat.computeAtk();
	}

	public override void OnRemove() {
		Game.player.stat.baseAtk -= attackBuff;
		Game.player.stat.computeAtk();
	}
}
