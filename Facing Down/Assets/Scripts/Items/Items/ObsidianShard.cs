using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsidianShard : Item
{

    public ObsidianShard() : base("ObsidianShard", ItemRarity.LEGENDARY, ItemType.FIRE) { }

	public override void OnEnemyKill(Entity enemy) {
		Laser bullet = new Laser("Enemy");
		
		Entity closestEnemy = null;
		foreach(Entity entity in GameObject.FindObjectsOfType<Entity>()) {
			if (entity.tag != "Enemy" || entity == enemy) continue;
			if (closestEnemy == null || (enemy.transform.position - entity.transform.position).magnitude < (enemy.transform.position - closestEnemy.transform.position).magnitude)
				closestEnemy = entity;
		}
		if (closestEnemy != null) {
			Debug.Log("DEAD : " + enemy.transform.position + " PLAYER : " + Game.player.self.transform.position + " ANGLE : " + Vector2.Angle(Game.player.self.transform.position, enemy.transform.position));
			bullet.Attack( - Vector2.SignedAngle(closestEnemy.transform.position - enemy.transform.position, Vector2.right), enemy);
		}
	}

	public override Item MakeCopy() {
		return new ObsidianShard();
	}
}
