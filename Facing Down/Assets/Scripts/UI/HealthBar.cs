using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Min(0)] public float maxWidth = 800;
	private float currentWidth;
	private float height;
	[Min(0)] public float pixelPerHP = 0.4f;

	RectTransform HPFill;
	Text healthText;

	private void Start() {
		HPFill = (RectTransform) transform.Find("HPFill").transform;
		healthText = GetComponentInChildren<Text>();
		height = ((RectTransform) transform).sizeDelta.y;
		UpdateHP();
	}

	public void UpdateHP() {
		currentWidth = Mathf.Min(maxWidth, Game.player.stat.GetMaxHP() * pixelPerHP);
		((RectTransform)transform).sizeDelta = new Vector2(currentWidth, height);
		healthText.text = Game.player.stat.GetCurrentHP() + " / " + Game.player.stat.GetMaxHP();
		HPFill.sizeDelta = new Vector2(currentWidth * Game.player.stat.GetCurrentHP() / Game.player.stat.GetMaxHP(), height);
	}
}
