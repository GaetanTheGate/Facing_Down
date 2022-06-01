using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonOptions : MonoBehaviour
{
    public static GameObject buttonApply;

    public void options(){
        MenuManager.gameObjectActions.SetActive(false);
        MenuManager.gameObjectOptions.SetActive(true);

        ButtonBack.gameObjectsToEnable.Add(MenuManager.gameObjectActions);
        MenuManager.gameObjectActions.GetComponent<InfoSelectButton>().selectButton = gameObject;
        ButtonBack.gameObjectsToDisable.Add(MenuManager.gameObjectOptions);

        loadOptions();
        buttonApply.SetActive(false);

        if(ToggleSelectableObject.onController)
            EventSystem.current.SetSelectedGameObject(GameObject.Find("DropdownLangue"));
        
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
        GameObject.Find("MasterVolumeValueText").GetComponent<Text>().text = ((GameObject.Find("SliderMasterVolume").GetComponent<Slider>().value + 80)*1.25f).ToString();
        
        GameObject.Find("SliderMusicVolume").GetComponent<Slider>().value = Options.Get().musicVolumeValue;
        GameObject.Find("MusicVolumeValueText").GetComponent<Text>().text = ((GameObject.Find("SliderMusicVolume").GetComponent<Slider>().value + 80)*1.25f).ToString();
        
        GameObject.Find("SliderSoundVolume").GetComponent<Slider>().value = Options.Get().soundVolumeValue;
        GameObject.Find("SoundVolumeValueText").GetComponent<Text>().text = ((GameObject.Find("SliderSoundVolume").GetComponent<Slider>().value + 80)*1.25f).ToString();
        ButtonAdjustVolume.contentVolume.SetActive(false);

        


        //load keyBinding
        ButtonDisplayCommand.contentDisplayCommands.SetActive(true);

        ButtonDisplayCommand.scrollRectContentDisplayCommandKeyBoard.SetActive(true);
        GameObject contentDisplayCommandKeyBoard = ButtonDisplayCommand.scrollRectContentDisplayCommandKeyBoard.transform.Find("Viewport").Find("ContentDisplayCommandKeyBoard").gameObject;

        foreach(KeyBinding keyBinding in Options.Get().commandsKeyBoard){
            foreach(Transform child in contentDisplayCommandKeyBoard.transform){
                GameObject action = child.Find("Action").gameObject;
                if(keyBinding.action == action.GetComponent<InfoAction>().idAction)
                    child.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text = keyBinding.key.ToString();
            }
        }

        ButtonDisplayCommand.scrollRectContentDisplayCommandKeyBoard.SetActive(false);

        ButtonDisplayCommand.scrollRectContentDisplayCommandController.SetActive(true);
       
        GameObject contentDisplayCommandController = ButtonDisplayCommand.scrollRectContentDisplayCommandController.transform.Find("Viewport").Find("ContentDisplayCommandController").gameObject;

        foreach(KeyBinding keyBinding in Options.Get().commandsController){
            foreach(Transform child in contentDisplayCommandController.transform){
                GameObject action = child.Find("Action").gameObject;
                if(keyBinding.action == action.GetComponent<InfoAction>().idAction)
                    child.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text = ButtonChangeCommand.keycodeControllerToSTring(keyBinding.key);
            }
        }
        ButtonDisplayCommand.scrollRectContentDisplayCommandController.SetActive(false);

        ButtonDisplayCommand.contentDisplayCommands.SetActive(false);
        
    }

    void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonOptions").TEXT;
    }
}
