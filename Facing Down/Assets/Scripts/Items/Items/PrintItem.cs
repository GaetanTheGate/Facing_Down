using UnityEngine;

public class PrintItem : Item
{
	public PrintItem() : base("Print", ItemRarity.LEGENDARY, ItemType.AIR) {}

	public override void OnPickup() {
		Debug.Log("Item Picked Up");
	}

	public override void OnRemove() {
		Debug.Log("Item Removed");
	}

	public override Item makeCopy() {
		return new PrintItem();
	}
}
