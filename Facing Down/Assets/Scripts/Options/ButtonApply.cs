using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ButtonApply : MonoBehaviour
{
    private string pathOptions = "Assets/Resources/Json/Options/Options.json";
    public void apply(){
        Options options = new Options();
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Option")){
            if(go.GetComponent<Dropdown>() != null)
                options.langue = go.GetComponent<Dropdown>().captionText.text;
        }

        string jsonStringOptions = JsonUtility.ToJson(options);

        File.WriteAllText(pathOptions,jsonStringOptions);

        print("options sauvegardé");

    }
}
