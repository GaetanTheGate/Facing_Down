using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPedestalPreviewArea : MonoBehaviour
{
	private static List<ItemPedestalPreviewArea> activePreviewAreas;

	private Item item;

	static ItemPedestalPreviewArea() {
		activePreviewAreas = new List<ItemPedestalPreviewArea>();
	}

	public void SetItem(Item item) {
		this.item = item;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.name == "PlayerEntity") activePreviewAreas.Add(this);
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.name == "PlayerEntity") activePreviewAreas.Remove(this);
	}

	private void Update() {
		if (UI.inventoryDisplay.IsEnabled()) return;
		if (activePreviewAreas.Count != 0) {
			if (!UI.itemPreview.gameObject.activeSelf) UI.itemPreview.gameObject.SetActive(true);

			float minDistToPlayer = float.MaxValue;
			ItemPedestalPreviewArea closest = null;
			foreach (ItemPedestalPreviewArea preview in activePreviewAreas) {
				float distToPlayer = (preview.transform.position - Game.player.self.transform.position).magnitude;
				if (distToPlayer < minDistToPlayer) {
					minDistToPlayer = distToPlayer;
					closest = preview;
				}
			}
			if (UI.itemPreview.GetCurrentItem() != closest.item) UI.itemPreview.SetItem(closest.item);
		}
		else if (UI.itemPreview.gameObject.activeSelf) UI.itemPreview.gameObject.SetActive(false);
	}

	private void OnDestroy() {
		if (activePreviewAreas.Contains(this)) activePreviewAreas.Remove(this);
		if (activePreviewAreas.Count == 0 && UI.itemPreview != null && UI.itemPreview.gameObject.activeSelf) UI.itemPreview.gameObject.SetActive(false);
	}
}
