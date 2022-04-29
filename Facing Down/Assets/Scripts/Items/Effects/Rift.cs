using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rift : MonoBehaviour
{
	private float accelerationBoost = 0;
	private float duration = 10f;
    public void Init(float boost, Vector2 position) {
		transform.position = position;
		accelerationBoost = boost;
	}

	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) Game.player.stat.ModifyAcceleration(accelerationBoost);
	}

	public void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("Player")) Game.player.stat.ModifyAcceleration(-accelerationBoost);
	}

	public void Update() {
		duration -= Time.deltaTime;
		if (duration < 0) Destroy(gameObject);
	}
}
