using UnityEngine;

public class PrintItem : Item
{
	public PrintItem() {
		ID = "Print";
	}

	public override void OnPickup() {
		Debug.Log("Item Picked Up");
	}

	public override void OnRemove() {
		Debug.Log("Item Removed");
	}
}
