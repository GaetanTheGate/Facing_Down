using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ButtonApply : MonoBehaviour
{
    
    public void apply(){
        Options options = new Options();
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Option")){
            if(go.name == "DropdownLangue")
                options.langue = go.GetComponent<Dropdown>().captionText.text;
            if(go.name == "SliderVolume")
                options.volumeValue = go.GetComponent<Slider>().value;
            if(go.name.Contains("Command")){
                KeyBinding keyBinding = new KeyBinding();
                keyBinding.action = go.transform.Find("Action").GetComponent<Text>().text;
                string stringKeyCode = go.transform.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text;
                keyBinding.key = (KeyCode) System.Enum.Parse(typeof(KeyCode), stringKeyCode);
                options.commands.Add(keyBinding);

            }
        }

        string jsonStringOptions = JsonUtility.ToJson(options);

        File.WriteAllText(MenuManager.pathOptions,jsonStringOptions);

        print("options sauvegardé");

    }
}
