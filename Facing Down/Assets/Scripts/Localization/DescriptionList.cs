using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List of localized descriptions
/// </summary>
/// <typeparam name="T"></typeparam>

[System.Serializable]
public class DescriptionList<T> where T : AbstractDescription
{
    public List<T> descriptions;

    /// <summary>
    /// Gets a dictionnary with IDs as keys from the descrption list
    /// </summary>
    /// <returns>The desctriptions stored as a dictionary</returns>
    public Dictionary<string, T> ToDictionary() {
        Dictionary<string, T> dictionary = new Dictionary<string, T>();
        foreach(T description in descriptions) {
            dictionary.Add(description.ID, description);
		}
        return dictionary;
	}
}
