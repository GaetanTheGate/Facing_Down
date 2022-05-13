using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behaviour setting the UI's ItemPreview to a given item when the player collides with this script's GameObject
/// </summary>
public class ItemPedestalPreviewArea : MonoBehaviour
{
	private static List<ItemPedestalPreviewArea> activePreviewAreas;

	private Item item;

	static ItemPedestalPreviewArea() {
		activePreviewAreas = new List<ItemPedestalPreviewArea>();
	}

	/// <summary>
	/// Sets the item that will be displayed in the UI.
	/// </summary>
	/// <param name="item"></param>
	public void SetItem(Item item) {
		this.item = item;
	}

	/// <summary>
	/// Adds this object to the list of currently active objects
	/// </summary>
	/// <param name="collision"></param>
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.name == "PlayerEntity") activePreviewAreas.Add(this);
	}

	/// <summary>
	/// Removes this object from the list of currently active objects
	/// </summary>
	/// <param name="collision"></param>
	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.name == "PlayerEntity") activePreviewAreas.Remove(this);
	}

	/// <summary>
	/// Updates the UI using the closest curently active previewArea to the player.
	/// </summary>
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

	/// <summary>
	/// On destroying this object, removes it from the active list, and hides the UI's ItemPreview if needed
	/// </summary>
	private void OnDestroy() {
		if (activePreviewAreas.Contains(this)) activePreviewAreas.Remove(this);
		if (activePreviewAreas.Count == 0 && UI.itemPreview != null && UI.itemPreview.gameObject.activeSelf) UI.itemPreview.gameObject.SetActive(false);
	}
}
