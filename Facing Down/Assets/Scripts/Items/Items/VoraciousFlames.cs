using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoraciousFlames : Item
{
	private bool isActive;
	private readonly float damageIncrease = 0.2f;

    public VoraciousFlames() : base("VoraciousFlames", ItemRarity.RARE, ItemType.FIRE) {
		isActive = false;
	}

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, damageIncrease * 100);
	}

	public override DamageInfo OnDealDamage(DamageInfo damage) {
		EntityCollisionStructure playerEntity = Game.player.self.GetComponent<EntityCollisionStructure>();
		if (isActive) damage.amount *= 1 + damageIncrease * amount;
		isActive = !(playerEntity.isCeilinged || playerEntity.isGrounded || playerEntity.isWalled);
		return damage;
	}

	public override void OnGroundCollisionEnter() {
		isActive = false;
	}

	public override Item MakeCopy() {
		return new VoraciousFlames();
	}
}
