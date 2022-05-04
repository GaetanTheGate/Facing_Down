using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float maxWidth;
	private float height;

	RectTransform healthFill;

	private void Start() {
		healthFill = (RectTransform) transform.Find("HPFill").transform;
		maxWidth = healthFill.sizeDelta.x;
		height = healthFill.sizeDelta.y;
	}

	public void UpdateHP() {
		healthFill.sizeDelta = new Vector2(maxWidth * Game.player.stat.GetCurrentHP() / Game.player.stat.GetMaxHP(), height);
	}
}
