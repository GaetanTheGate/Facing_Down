using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Localization
{
    private static readonly string localizationPath = "Json/Localization/";

    private static Dictionary<string, ItemDescription> itemDescriptions;
    private static Dictionary<string, UIString> UIStrings;

    /// <summary>
    /// Initializes all dictionaries
    /// </summary>
    static Localization() {
        Init();
	}

    public static void Init() {
        if (itemDescriptions != null) return;
        InitItemDescriptions(Options.Get().langue);
    }

    private static void InitItemDescriptions(string lang) {
        LocalizedTextList<ItemDescription> descriptionList = JsonUtility.FromJson<LocalizedTextList<ItemDescription>>(Resources.Load<TextAsset>(localizationPath + lang + "/ItemDescriptions").text);
        itemDescriptions = descriptionList.ToDictionary();
    }

    private static void InitUIStrings(string lang) {
        LocalizedTextList<UIString> stringList = JsonUtility.FromJson<LocalizedTextList<UIString>>(Resources.Load<TextAsset>(localizationPath + lang + "/UIStrings").text);
        UIStrings = stringList.ToDictionary();

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

    public static UIString GetUIString(string ID) {
        if (UIStrings.ContainsKey(ID))
            return UIStrings[ID];

        return new UIString();
	}
}

