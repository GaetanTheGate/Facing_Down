using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Localization
{
    private static readonly string localizationPath = "Json/Localization/";

    private static Dictionary<string, ItemDescription> itemDescriptions;
    private static Dictionary<string, ItemDescription> weaponDescriptions;

    /// <summary>
    /// Initializes all dictionaries
    /// </summary>
    static Localization() {
        InitItemDescriptions("fr");
	}

    private static void InitItemDescriptions(string lang) {
        itemDescriptions = JsonUtility.FromJson<DescriptionList<ItemDescription>>(Resources.Load<TextAsset>(localizationPath + lang + "/ItemDescriptions").text).ToDictionary();
        weaponDescriptions = JsonUtility.FromJson<DescriptionList<ItemDescription>>(Resources.Load<TextAsset>(localizationPath + lang + "/WeaponDescriptions").text).ToDictionary();
    }

    /// <summary>
    /// Returns a specific item's description
    /// </summary>
    /// <param name="ID">The item's ID</param>
    /// <returns>The corresponding ItemDescription</returns>
    public static ItemDescription GetItemDescription(string ID) {
        if(itemDescriptions.ContainsKey(ID))
            return itemDescriptions[ID];

        return new ItemDescription();
	}

    public static ItemDescription GetWeaponDescription(string ID) {
        if (weaponDescriptions.ContainsKey(ID))
            return weaponDescriptions[ID];

        return new ItemDescription();
	}
}

