using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ButtonApply : MonoBehaviour
{
    public static bool onDisplayCommand = false;
    public void apply(){

        ButtonDisplayCommand.contentDisplayCommand.SetActive(true);

        Options.Get().langue = GameObject.Find("DropdownLangue").GetComponent<Dropdown>().captionText.text;
        Options.Get().volumeValue = GameObject.Find("SliderVolume").GetComponent<Slider>().value;
        GameObject commands = GameObject.Find("ContentDisplayCommand").gameObject;
        for (int i = 0; i < commands.transform.childCount; ++i) {
            GameObject command = commands.transform.GetChild(i).gameObject;
            foreach (KeyBinding keyBinding in Options.Get().commands) {
                if (keyBinding.action == command.transform.Find("Action").GetComponent<InfoAction>().idAction) {
                    string stringKeyCode = command.transform.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text;
                    keyBinding.key = (KeyCode)System.Enum.Parse(typeof(KeyCode), stringKeyCode);
                    break;
                }
            }
        }
        
        if(!onDisplayCommand){
            ButtonDisplayCommand.contentDisplayCommand.SetActive(false);
            onDisplayCommand = false;
        }
            

        Options.Save();

        print("options sauvegardées");

        gameObject.SetActive(false);

    }

    void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonApply").TEXT;
    }
}
