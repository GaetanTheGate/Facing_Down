using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityCloak : Item
{
	private readonly float invulnerabilityTime = 0.5f;
	private readonly float stackInvulnerabilityTime = 0.25f;
    public InvisibilityCloak() : base("InvisibilityCloak", ItemRarity.LEGENDARY, ItemType.WIND) { }

	private void Activate() {
		Game.player.self.GetComponent<PlayerIframes>().getIframeItem(invulnerabilityTime + (amount - 1) * stackInvulnerabilityTime);
	}

	public override void OnDash() {
		Activate();
	}
	public override void OnMegaDash() {
		Activate();
	}
	public override void OnRedirect() {
		Activate();
	}

	public override Item MakeCopy() {
		return new InvisibilityCloak();
	}
}
