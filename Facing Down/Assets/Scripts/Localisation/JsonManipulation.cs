using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor.Build.Content;

public class JsonManipulation : MonoBehaviour
{
    public static ObjectsDescription GetItemsDescriptionFromJsonFile(string langue){
        print(Resources.Load<TextAsset>("Json/ItemsDescription_"+langue).text);
        return JsonUtility.FromJson<ObjectsDescription>(Resources.Load<TextAsset>("Json/ItemsDescription_"+langue).text);
    }
    
    
}

