using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ButtonApply : MonoBehaviour
{
    public static bool onDisplayCommands = false;
    public static bool onContentVolume = false;
    public void apply(){

        ButtonDisplayCommand.contentDisplayCommands.SetActive(true);
        ButtonDisplayCommand.scrollRectContentDisplayCommandKeyBoard.SetActive(true);
        ButtonDisplayCommand.scrollRectContentDisplayCommandController.SetActive(true);
        ButtonAdjustVolume.contentVolume.SetActive(true);

        Options.Get().langue = GameObject.Find("DropdownLangue").GetComponent<Dropdown>().captionText.text;

        Options.Get().masterVolumeValue = GameObject.Find("SliderMasterVolume").GetComponent<Slider>().value;
        Options.Get().musicVolumeValue = GameObject.Find("SliderMusicVolume").GetComponent<Slider>().value;
        Options.Get().soundVolumeValue = GameObject.Find("SliderSoundVolume").GetComponent<Slider>().value;

        GameObject commandsKeyBoard = GameObject.Find("ContentDisplayCommandKeyBoard").gameObject;
        for (int i = 0; i < commandsKeyBoard.transform.childCount; ++i) {
            GameObject command = commandsKeyBoard.transform.GetChild(i).gameObject;
            foreach (KeyBinding keyBinding in Options.Get().commandsKeyBoard) {
                if (keyBinding.action == command.transform.Find("Action").GetComponent<InfoAction>().idAction) {
                    string stringKeyCode = command.transform.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text;
                    keyBinding.key = (KeyCode)System.Enum.Parse(typeof(KeyCode), stringKeyCode);
                    break;
                }
            }
        }

        GameObject commandsController = GameObject.Find("ContentDisplayCommandController").gameObject;
        for (int i = 0; i < commandsController.transform.childCount; ++i) {
            GameObject command = commandsController.transform.GetChild(i).gameObject;
            foreach (KeyBinding keyBinding in Options.Get().commandsController) {
                if (keyBinding.action == command.transform.Find("Action").GetComponent<InfoAction>().idAction) {
                    keyBinding.key = ButtonChangeCommand.stringToKeyCode(command.transform.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text);
                    break;
                }
            }
        }

        foreach(GameObject go in ControllerManager.typeController){
            go.SetActive(false);
        }
        ControllerManager.currentControl.SetActive(true);

        
        
        if(!onDisplayCommands){
            ButtonDisplayCommand.contentDisplayCommands.SetActive(false);
            onDisplayCommands = false;
        }

        if(!onContentVolume){
            ButtonAdjustVolume.contentVolume.SetActive(false);
            onContentVolume = false;
        }
            

        Options.Save();

        print("options sauvegardées");

        gameObject.SetActive(false);

    }

    void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonApply").TEXT;
    }
}
