using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rift : MonoBehaviour
{
	private float accelerationBoost = 0;
	private float duration = 2f;

	private static int activeRiftBoosts = 0;
    public void Init(float boost, Vector2 position) {
		transform.position = position;
		accelerationBoost = boost;
	}

	public void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			if (activeRiftBoosts == 0) {
				Game.player.stat.ModifyAcceleration(accelerationBoost);
			}
			activeRiftBoosts += 1;
		}
	}

	public void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			activeRiftBoosts -= 1;
			if (activeRiftBoosts == 0) {
				Game.player.stat.ModifyAcceleration(-accelerationBoost);
			}
		}
	}

	public void Update() {
		duration -= Time.deltaTime;
		if (duration < 0) Destroy(gameObject);
	}
}
