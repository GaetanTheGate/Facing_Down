using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsidianShard : Item
{

    public ObsidianShard() : base("ObsidianShard", ItemRarity.LEGENDARY, ItemType.FIRE) { }

	public override void OnEnemyKill(Entity enemy) {
		Bullet bullet = new Bullet("Enemy");
		
		Entity closestEnemy = null;
		foreach(Entity entity in GameObject.FindObjectsOfType<Entity>()) {
			if (entity.tag != "Enemy" || entity == enemy) continue;
			if (closestEnemy == null || (enemy.transform.position - entity.transform.position).magnitude < (enemy.transform.position - closestEnemy.transform.position).magnitude)
				closestEnemy = entity;
		}
		if (closestEnemy != null) {
			Debug.Log("DEAD : " + enemy + " CLOSEST : " + closestEnemy + " ANGLE : " + Vector3.Angle(enemy.transform.position, closestEnemy.transform.position));
			bullet.Attack(Vector3.Angle(closestEnemy.transform.position, enemy.transform.position) - 90, enemy);
		}
	}

	public override Item MakeCopy() {
		return new ObsidianShard();
	}
}
