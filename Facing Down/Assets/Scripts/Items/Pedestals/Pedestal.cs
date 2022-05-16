using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pedestal : MonoBehaviour
{
	private ItemChoice choice;


	/// <summary>
	/// Registers this pedestal as a choice for the chosen ItemChoice
	/// </summary>
	/// <param name="choice"></param>
	public void SetItemChoice(ItemChoice choice) {
		this.choice = choice;
	}

	/// <summary>
	/// Disables this pedestal and the ones linked by an itemChoice.
	/// </summary>
	public virtual void DisablePedestal() {
		if (choice != null) choice.DisablePedestals();
	}
}
