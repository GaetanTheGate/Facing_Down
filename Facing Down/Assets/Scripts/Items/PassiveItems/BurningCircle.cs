using UnityEngine;

public class BurningCircle : PassiveItem {

	private readonly float damage = 50f;
	private readonly float rangePerStack = 5f;
	public BurningCircle() : base("BurningCircle", ItemRarity.UNCOMMON, ItemType.FIRE) { }

	MeleeWeapon weapon;

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, damage);
	}
	public override void OnPickup() {
		if (weapon == null) {
			weapon = new Explosion("Enemy");
			weapon.SetDmgType(DamageType.SECONDARY);
			weapon.SetBaseAtk(damage);
		}
		weapon.SetBaseRange(rangePerStack * amount);
	}

	public override void OnRemove() {
		weapon.SetBaseRange(rangePerStack * amount);
	}

	public override DamageInfo OnDealDamage(DamageInfo damage) {
		if (damage.type == DamageType.PRIMARY) {
			weapon.startPos = damage.target.transform.position;
			weapon.WeaponAttack(0, Game.player.self);
		}
		return damage;
	}
}
