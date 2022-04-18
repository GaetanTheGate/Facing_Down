using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour {
    public static readonly string spriteFolderPath = "Items/";
    static readonly float imageSize = Screen.width / 20;
    Item item;
    public void Init(Item item) {
        this.item = item;
        transform.position = new Vector3(150, 150, 0);

        GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(spriteFolderPath + item.getID());
        GetComponentInChildren<Image>().rectTransform.sizeDelta = new Vector2(imageSize, imageSize);
        GetComponentInChildren<Text>().text = item.getAmount().ToString();
        GetComponentInChildren<Text>().rectTransform.sizeDelta = new Vector2(imageSize, imageSize);
    }

    public void setPosition(Vector2 pos) {
        GetComponentInChildren<Image>().rectTransform.position = pos;
        GetComponentInChildren<Text>().rectTransform.position = pos;
    }

    public void update() {
        GetComponentInChildren<Text>().text = item.getAmount().ToString();
    }

	public void OnDestroy() {
        foreach (Transform child in transform) Destroy(child.gameObject);
	}
}
