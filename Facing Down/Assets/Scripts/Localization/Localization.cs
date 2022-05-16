using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Localization
{
    private static readonly string localizationPath = "Json/Localization/";

    private static Dictionary<string, ItemDescription> itemDescriptions;

    /// <summary>
    /// Initializes all dictionaries
    /// </summary>
    static Localization() {
        InitItemDescriptions("fr");
	}

    private static void InitItemDescriptions(string lang) {
        DescriptionList<ItemDescription> descriptionList = JsonUtility.FromJson<DescriptionList<ItemDescription>>(Resources.Load<TextAsset>(localizationPath + lang + "/ItemDescriptions").text);
        itemDescriptions = descriptionList.ToDictionary();
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
}

