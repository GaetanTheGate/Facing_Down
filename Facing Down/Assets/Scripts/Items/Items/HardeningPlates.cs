using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardeningPlates : Item
{
    private readonly float invulnerabilityTime = 0.25f;
    public HardeningPlates() : base("HardeningPlates", ItemRarity.UNCOMMON, ItemType.EARTH) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, invulnerabilityTime);
	}

	public override void OnEnemyKill(Entity enemy) {
		Game.player.GetComponent<PlayerIframes>().getIframeItem(invulnerabilityTime * amount);
	}

	public override Item MakeCopy() {
		return new HardeningPlates();
	}
}
