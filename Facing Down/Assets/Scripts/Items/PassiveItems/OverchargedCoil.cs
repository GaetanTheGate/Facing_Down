using UnityEngine;

public class OverchargedCoil : PassiveItem {

	private readonly float damagePerStack = 50f;
	private readonly float cooldown = 1f;
	private float lastAttackTime;
	public OverchargedCoil() : base("OverchargedCoil", ItemRarity.LEGENDARY, ItemType.THUNDER) { }

	MeleeWeapon weapon;

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, amount * damagePerStack);
	}
	public override void OnPickup() {
		if (weapon == null) {
			weapon = new WipeOut("Enemy");
			lastAttackTime = 0;
		}
		weapon.SetBaseAtk(damagePerStack * amount);
	}

	public override void OnRemove() {
		weapon.SetBaseAtk(damagePerStack * amount);
	}

	public override void OnBullettimeActivate() {
		if (Time.time < lastAttackTime + cooldown) return;
		weapon.Attack(0f, Game.player.self);
		lastAttackTime = Time.time;
	}
}
