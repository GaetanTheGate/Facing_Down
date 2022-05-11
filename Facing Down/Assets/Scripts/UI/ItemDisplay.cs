using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	private static ItemDisplay prefab;
	private Item item;

	public static ItemDisplay InstantiateItemDisplay(Item item, Transform transform, Vector2 localPosition) {
		if (prefab == null) prefab = Resources.Load<ItemDisplay>("Prefabs/UI/Components/ItemDisplay");

		ItemDisplay itemDisplay = Instantiate<ItemDisplay>(prefab, transform);
		itemDisplay.item = item;
		itemDisplay.GetComponentInChildren<Image>().sprite = item.GetSprite();
		itemDisplay.GetComponentInChildren<Text>().text = item.GetAmount().ToString();
		itemDisplay.transform.localPosition = localPosition;

		return itemDisplay;
	}

	public void UpdateAmount() {
		this.GetComponentInChildren<Text>().text = item.GetAmount().ToString();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		UI.itemPreview.gameObject.SetActive(true);
		UI.itemPreview.SetItem(item);
	}

	public void OnPointerExit(PointerEventData eventData) {
		UI.itemPreview.gameObject.SetActive(false);
	}
}
