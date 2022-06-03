using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Behaviour for the UI element displaying an item in the inventory
/// </summary>
public class ItemDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler {
	private static ItemDisplay prefab;
	private PassiveItem item;

	/// <summary>
	/// Instantiates a new item display.
	/// </summary>
	/// <param name="item"></param>
	/// <param name="parent">The parent's tranform</param>
	/// <param name="localPosition">The item display's position in the parent</param>
	/// <returns></returns>
	public static ItemDisplay InstantiateItemDisplay(PassiveItem item, Transform parent, Vector2 localPosition) {
		if (prefab == null) prefab = Resources.Load<ItemDisplay>("Prefabs/UI/Components/ItemDisplay");

		ItemDisplay itemDisplay = Instantiate<ItemDisplay>(prefab, parent);
		itemDisplay.item = item;
		itemDisplay.GetComponentInChildren<Image>().sprite = item.GetSprite();
		itemDisplay.GetComponentInChildren<Text>().text = item.GetAmount().ToString();
		itemDisplay.transform.localPosition = localPosition;

		return itemDisplay;
	}

	public void UpdateAmount() {
		this.GetComponentInChildren<Text>().text = item.GetAmount().ToString();
	}

	//Displays or hide the UI's ItemPreview when the pointer hovers the item
	public void OnPointerEnter(PointerEventData eventData) {
		enableItemDisplay();
	}

	public void OnPointerExit(PointerEventData eventData) {
		disableItemDisplay();
	}

	public void OnSelect(BaseEventData data){
		enableItemDisplay();
	}

	public void OnDeselect(BaseEventData data){
		disableItemDisplay();
	}

	public void enableItemDisplay(){
		UI.itemPreview.SetItem(item);
	}

	public void disableItemDisplay(){
		UI.itemPreview.SetItem(Game.player.inventory.GetWeapon());
	}
}
