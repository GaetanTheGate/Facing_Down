using UnityEngine;

public class PrintItem : Item
{
	public PrintItem() : base("PrintItem", ItemRarity.LEGENDARY, ItemType.WIND) {}

	public override void OnPickup() {
		Debug.Log("Item Picked Up");
	}

	public override void OnRemove() {
		Debug.Log("Item Removed");
	}

	public override DamageInfo OnTakeDamage(DamageInfo damage) {
		Debug.Log("DAMAGE TAKEN");
		return damage;
	}
	public override DamageInfo OnDealDamage(DamageInfo damage) {
		Debug.Log("DAMAGE DEALT");
		return damage;
	}

	public override bool OnDeath() {
		Debug.Log("DYING");
		return false;
	}

	public override void OnEnemyKill() {
		Debug.Log("ENEMY KILLED");
	}

	public override void OnGroundCollisionEnter(Collision collision) {
		Debug.Log("GROUND COLLISION ENTERED");
	}

	public override void OnGroundCollisionLeave(Collision collision) {
		Debug.Log("GROUND COLLISION LEFT");
	}

	public override void OnBullettimeActivate() {
		Debug.Log("BULLET TIME ACTIVATED");
	}

	public override void OnRoomFinish() {
		Debug.Log("ROOM FINISHED");
	}

	public override void OnDash() {
		Debug.Log("DASHED");
	}

	public override void OnMegaDash() {
		Debug.Log("MEGADASHED");
	}

	public override void OnRedirect() {
		Debug.Log("REDIRECTION USED");
	}


	public override Item MakeCopy() {
		return new PrintItem();
	}
}
