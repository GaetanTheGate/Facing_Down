using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Localization
{
    private static readonly string localizationPath = "Json/Localization/";

    private static Dictionary<string, ItemDescription> itemDescriptions;

    static Localization() {
        InitItemDescriptions("fr");
	}

    private static void InitItemDescriptions(string lang) {
        DescriptionList<ItemDescription> descriptionList = JsonUtility.FromJson<DescriptionList<ItemDescription>>(Resources.Load<TextAsset>(localizationPath + lang + "/ItemDescriptions").text);
        itemDescriptions = descriptionList.ToDictionary();
    }

    public static ItemDescription GetItemDescription(string ID) {
        return itemDescriptions[ID];
	}
}

