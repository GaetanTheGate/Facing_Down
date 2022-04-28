using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiftMaker : Item
{
	private float accelerationBoost = 3f;
    public RiftMaker() : base("RiftMaker", ItemRarity.EPIC, ItemType.WIND) { }

	private void CreateRift() {
		GameObject.Instantiate<Rift>(Resources.Load<Rift>("Prefabs / Items / ItemEffects / Rift")).Init(accelerationBoost * amount, Game.player.self.transform.position);
	}

	public override void OnGroundCollisionEnter(Collision collision) {
		CreateRift();
	}

	public override void OnGroundCollisionLeave(Collision collision) {
		CreateRift();
	}

	public override Item MakeCopy() {
		return new RiftMaker();
	}
}
