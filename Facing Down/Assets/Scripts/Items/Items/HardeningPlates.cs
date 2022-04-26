using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardeningPlates : Item
{
    private readonly float invulnerabilityTime = 0.25f;
    public HardeningPlates() : base("HardeningPlates", ItemRarity.UNCOMMON, ItemType.EARTH) { }

	public override void OnEnemyKill() {
		Game.player.GetComponent<PlayerIframes>().getIframeItem(invulnerabilityTime * amount);
	}

	public override Item MakeCopy() {
		return new HardeningPlates();
	}
}
