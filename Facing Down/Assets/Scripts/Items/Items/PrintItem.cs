using UnityEngine;

public class PrintItem : Item
{
	public PrintItem() : base("PrintItem", ItemRarity.LEGENDARY, ItemType.AIR) {}

	public override void OnPickup() {
		Debug.Log("Item Picked Up");
	}

	public override void OnRemove() {
		Debug.Log("Item Removed");
	}

	public override float OnTakeDamage(float damage) {
		Debug.Log("DAMAGE TAKEN !");
		return damage;
	}

	public override Item makeCopy() {
		return new PrintItem();
	}
}
