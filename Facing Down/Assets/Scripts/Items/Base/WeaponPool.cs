using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponPool {
	private static Dictionary<string, Weapon> weapons;

	/// <summary>
	/// Initializes values
	/// </summary>
	/// New Weapons should be added here.
	static WeaponPool() {
		weapons = new Dictionary<string, Weapon>();

		Add(new Katana());
		Add(new Daggers());
		Add(new Gun());
		Add(new WarAxe());
		Add(new Wings());
		Add(new Shuriken());
	}

	/// <summary>
	/// Adds a Weapon to the pool.
	/// </summary>
	/// <param name="weapon">The weapon to add.</param>
	private static void Add(Weapon weapon) {
		weapons.Add(weapon.GetID(), weapon);
	}

	/// <summary>
	/// Returns the weapon correspoding to an ID.
	/// </summary>
	/// <param name="id">The weapon's ID.</param>
	/// <returns>The weapon if found, else returns null.</returns>
	public static Weapon GetByID(string id) {
		if (!weapons.ContainsKey(id)) return null;
		return weapons[id];
	}

	/// <summary>
	/// Get a random weapon.
	/// </summary>
	/// <returns>A randomly choosen weapon from the pool.</returns>
	public static Weapon GetRandomItem() {
		return new List<Weapon>(weapons.Values)[Game.random.Next() % weapons.Count];
	}
}
