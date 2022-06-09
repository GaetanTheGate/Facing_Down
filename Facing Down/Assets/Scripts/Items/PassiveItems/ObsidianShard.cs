using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsidianShard : PassiveItem
{
	private readonly float baseAtk = 50f;
    public ObsidianShard() : base("ObsidianShard", ItemRarity.LEGENDARY, ItemType.FIRE) { }

	public override string GetDescription() {
		return string.Format(description.DESCRIPTION, baseAtk * amount);
	}

	public override void OnEnemyKill(Entity enemy) {
		Laser laser = new Laser("Enemy");
		laser.SetBaseAtk(baseAtk * amount);
		
		Entity closestEnemy = null;
		foreach(Entity entity in GameObject.FindObjectsOfType<Entity>()) {
			if (entity.tag != "Enemy" || entity == enemy || entity.gameObject.GetComponent<StatEntity>() == null || entity.gameObject.GetComponent<StatEntity>().getIsDead()) continue;
			if (closestEnemy == null || (enemy.transform.position - entity.transform.position).magnitude < (enemy.transform.position - closestEnemy.transform.position).magnitude)
				closestEnemy = entity;
		}
		if (closestEnemy != null) {
			laser.startPos = enemy.transform.position;
			laser.forceUnFollow = false;
			laser.WeaponAttack( - Vector2.SignedAngle(closestEnemy.transform.position - enemy.transform.position, Vector2.right), Game.player.self);
		}
	}
}
