using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ButtonApply : MonoBehaviour
{
    public void apply(){
        
        ButtonDisplayCommand.contentDisplayCommand.SetActive(true);
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Option")){
            if(go.name == "DropdownLangue")
                MenuManager.options.langue = go.GetComponent<Dropdown>().captionText.text;
            if(go.name == "SliderVolume")
                MenuManager.options.volumeValue = go.GetComponent<Slider>().value;
            if(go.name.Contains("Command")){
                /*KeyBinding keyBinding = new KeyBinding();
                keyBinding.action = go.transform.Find("Action").GetComponent<InfoAction>().idAction;
                string stringKeyCode = go.transform.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text;
                keyBinding.key = (KeyCode) System.Enum.Parse(typeof(KeyCode), stringKeyCode);
                options.commands.Add(keyBinding);*/
                foreach(KeyBinding keyBinding in MenuManager.options.commands){
                    if(keyBinding.action == go.transform.Find("Action").GetComponent<InfoAction>().idAction){
                        string stringKeyCode = go.transform.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text;
                        keyBinding.key = (KeyCode) System.Enum.Parse(typeof(KeyCode), stringKeyCode);
                        break;
                    }
                }
                
            }
        }
        
        ButtonDisplayCommand.contentDisplayCommand.SetActive(false);

        string jsonStringOptions = JsonUtility.ToJson(MenuManager.options);

        File.WriteAllText(MenuManager.pathOptions,jsonStringOptions);

        print("options sauvegardé");

    }
}
