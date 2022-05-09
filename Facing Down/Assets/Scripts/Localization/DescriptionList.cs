using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class DescriptionList<T> where T : AbstractDescription
{
    public List<T> descriptions;

    public Dictionary<string, T> ToDictionary() {
        Dictionary<string, T> dictionary = new Dictionary<string, T>();
        foreach(T description in descriptions) {
            dictionary.Add(description.ID, description);
		}
        return dictionary;
	}
}
