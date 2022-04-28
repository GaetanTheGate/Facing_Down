using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsidianShard : Item
{
	private readonly float baseAtk = 50f;
    public ObsidianShard() : base("ObsidianShard", ItemRarity.LEGENDARY, ItemType.FIRE) { }

	public override void OnEnemyKill(Entity enemy) {
		QuickLaser laser = new QuickLaser("Enemy");
		laser.SetBaseAtk(baseAtk * amount);
		
		Entity closestEnemy = null;
		foreach(Entity entity in GameObject.FindObjectsOfType<Entity>()) {
			if (entity.tag != "Enemy" || entity == enemy || entity.gameObject.GetComponent<StatEntity>() == null || entity.gameObject.GetComponent<StatEntity>().getIsDead()) continue; if (closestEnemy == null || (enemy.transform.position - entity.transform.position).magnitude < (enemy.transform.position - closestEnemy.transform.position).magnitude)
				closestEnemy = entity;
		}
		if (closestEnemy != null) {
			Debug.Log("DEAD : " + enemy.transform.position + " PLAYER : " + Game.player.self.transform.position + " ANGLE : " + Vector2.Angle(Game.player.self.transform.position, enemy.transform.position));
			laser.Attack( - Vector2.SignedAngle(closestEnemy.transform.position - enemy.transform.position, Vector2.right), enemy);
		}
	}

	public override Item MakeCopy() {
		return new ObsidianShard();
	}
}
