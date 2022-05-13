using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOptions : MonoBehaviour
{
    public void options(){
        MenuManager.gameObjectActions.SetActive(false);
        MenuManager.gameObjectOptions.SetActive(true);
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

    }
}
