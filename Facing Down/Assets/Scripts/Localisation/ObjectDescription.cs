using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectDescription 
{
    public string idItem;

    public List<ElementDescription> elementsDescription = new List<ElementDescription>();

    public ElementDescription GetElementDescription(string elementID){
        foreach(ElementDescription elementDescription in elementsDescription){
            if(elementDescription.elementID == elementID)
                return elementDescription;
        }
        return null;
    }
}
