using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintItem : Item
{
	static PrintItem() {
		Name = "Pickup Item";
	}

	public override void OnPickup() {
		Debug.Log("Item Picked Up");
	}

	public override void OnRemove() {
		Debug.Log("Item Removed");
	}
}
