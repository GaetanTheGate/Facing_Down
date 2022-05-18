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
        Slider sliderVolume = GameObject.Find("SliderVolume").GetComponent<Slider>();
        sliderVolume.value = Options.Get().volumeValue;


        //load keyBinding
        ButtonDisplayCommand.contentDisplayCommand.SetActive(true);
        foreach(KeyBinding keyBinding in Options.Get().commands){
            foreach(Transform child in ButtonDisplayCommand.contentDisplayCommand.transform){
                GameObject action = child.Find("Action").gameObject;
                if(keyBinding.action == action.GetComponent<InfoAction>().idAction)
                    child.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text = keyBinding.key.ToString();
            }
        }
        ButtonDisplayCommand.contentDisplayCommand.SetActive(false);
        
    }

    void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonOptions").TEXT;
    }
}
