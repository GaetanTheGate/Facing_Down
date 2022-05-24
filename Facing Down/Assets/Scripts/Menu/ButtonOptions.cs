using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOptions : MonoBehaviour
{
    public static GameObject buttonApply;

    public void options(){
        MenuManager.gameObjectActions.SetActive(false);
        MenuManager.gameObjectOptions.SetActive(true);

        ButtonBack.gameObjectsToEnable.Add(MenuManager.gameObjectActions);
        ButtonBack.gameObjectsToDisable.Add(MenuManager.gameObjectOptions);

        loadOptions();
        buttonApply.SetActive(false);
        
    }

    public static void loadOptions(){
        


        //load langue
        Dropdown dropdownLangue = GameObject.Find("DropdownLangue").GetComponent<Dropdown>();
        foreach(Dropdown.OptionData optionData in dropdownLangue.options){
            if(optionData.text == Options.Get().langue){
                dropdownLangue.value = dropdownLangue.options.IndexOf(optionData);
                break;  
            }
        }

        //load volume value
        ButtonAdjustVolume.contentVolume.SetActive(true);
        GameObject.Find("SliderMasterVolume").GetComponent<Slider>().value = Options.Get().masterVolumeValue;
        GameObject.Find("SliderMusicVolume").GetComponent<Slider>().value = Options.Get().musicVolumeValue;
        GameObject.Find("SliderSoundVolume").GetComponent<Slider>().value = Options.Get().soundVolumeValue;
        ButtonAdjustVolume.contentVolume.SetActive(false);

        


        //load keyBinding
        ButtonDisplayCommand.contentDisplayCommandKeyBoard.SetActive(true);
        foreach(KeyBinding keyBinding in Options.Get().commandsKeyBoard){
            foreach(Transform child in ButtonDisplayCommand.contentDisplayCommandKeyBoard.transform){
                GameObject action = child.Find("Action").gameObject;
                if(keyBinding.action == action.GetComponent<InfoAction>().idAction)
                    child.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text = keyBinding.key.ToString();
            }
        }
        ButtonDisplayCommand.contentDisplayCommandKeyBoard.SetActive(false);

        ButtonDisplayCommand.contentDisplayCommandController.SetActive(true);
        foreach(KeyBinding keyBinding in Options.Get().commandsController){
            foreach(Transform child in ButtonDisplayCommand.contentDisplayCommandController.transform){
                GameObject action = child.Find("Action").gameObject;
                if(keyBinding.action == action.GetComponent<InfoAction>().idAction)
                    child.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text = keyBinding.key.ToString();
            }
        }
        ButtonDisplayCommand.contentDisplayCommandController.SetActive(false);
        
    }

    void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonOptions").TEXT;
    }
}
