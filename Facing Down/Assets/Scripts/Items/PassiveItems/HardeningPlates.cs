using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardeningPlates : PassiveItem
{
    private readonly float invulnerabilityTime = 0.25f;
    public HardeningPlates() : base("HardeningPlates", ItemRarity.UNCOMMON, ItemType.EARTH) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, invulnerabilityTime * amount);
	}

	public override void OnEnemyKill(Entity enemy) {
		Game.player.self.GetComponent<PlayerIframes>().getIframeItem(invulnerabilityTime * amount);
	}
}
