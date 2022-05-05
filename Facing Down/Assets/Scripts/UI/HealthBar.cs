using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float maxWidth;
	private float height;

	RectTransform healthFill;
	Text healthText;

	private void Start() {
		healthFill = (RectTransform) transform.Find("HPFill").transform;
		healthText = GetComponentInChildren<Text>();
		maxWidth = healthFill.sizeDelta.x;
		height = healthFill.sizeDelta.y;
		UpdateHP();
	}

	public void UpdateHP() {
		Debug.Log("HP UPDATED");
		healthText.text = Game.player.stat.GetCurrentHP() + " / " + Game.player.stat.GetMaxHP();
		healthFill.sizeDelta = new Vector2(maxWidth * Game.player.stat.GetCurrentHP() / Game.player.stat.GetMaxHP(), height);
	}
}
