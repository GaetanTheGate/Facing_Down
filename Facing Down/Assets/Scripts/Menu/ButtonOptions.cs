using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOptions : MonoBehaviour
{
    public void options(){
        MenuManager.gameObjectActions.SetActive(false);
        MenuManager.gameObjectOptions.SetActive(true);

        ButtonBack.gameObjectsToEnable.Add(MenuManager.gameObjectActions);
        ButtonBack.gameObjectsToDisable.Add(MenuManager.gameObjectOptions);

        loadOptions();
    }

    public static void loadOptions(){

        //load langue
        Dropdown dropdownLangue = GameObject.Find("DropdownLangue").GetComponent<Dropdown>();
        foreach(Dropdown.OptionData optionData in dropdownLangue.options){
            if(optionData.text == MenuManager.options.langue){
                dropdownLangue.value = dropdownLangue.options.IndexOf(optionData);
                break;  
            }
        }

        //load volume value
        Slider sliderVolume = GameObject.Find("SliderVolume").GetComponent<Slider>();
        sliderVolume.value = MenuManager.options.volumeValue;


        //load keyBinding
        ButtonDisplayCommand.contentDisplayCommand.SetActive(true);
        foreach(KeyBinding keyBinding in MenuManager.options.commands){
            foreach(Transform child in ButtonDisplayCommand.contentDisplayCommand.transform){
                GameObject action = child.Find("Action").gameObject;
                if(keyBinding.action == action.GetComponent<InfoAction>().idAction)
                    child.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text = keyBinding.key.ToString();
            }
        }
        ButtonDisplayCommand.contentDisplayCommand.SetActive(false);
        
    }
}
