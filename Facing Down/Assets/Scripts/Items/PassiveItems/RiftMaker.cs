using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftMaker : PassiveItem
{
	private float accelerationBoost = 3f;
    public RiftMaker() : base("RiftMaker", ItemRarity.EPIC, ItemType.WIND) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, accelerationBoost * 10);
	}

	private void CreateRift() {
		GameObject.Instantiate<Rift>(Resources.Load<Rift>("Prefabs/Items/ItemEffects/Rift")).Init(accelerationBoost * amount, Game.player.self.transform.position);
	}

	public override void OnGroundCollisionEnter() {
		CreateRift();
	}

	public override void OnGroundCollisionLeave() {
		CreateRift();
	}
}