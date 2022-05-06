using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectsDescription
{
    public List<ObjectDescription> objectsDescription = new List<ObjectDescription>(); 

    public ObjectDescription GetObjectDescription(string idItem){
        foreach(ObjectDescription objectDescription in objectsDescription){
            if(objectDescription.idItem == idItem)
                return objectDescription;
        }
        return null;
    }


}
