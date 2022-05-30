using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTextValueSliderMasterVolume : MonoBehaviour
{
    public void updateMasterVolumeValue(){
        transform.Find("MasterVolumeValueText").GetComponent<Text>().text = ((GetComponent<Slider>().value + 80)*1.25f).ToString();
        MenuManager.audioMixer.SetFloat("masterVolume",GetComponent<Slider>().value);
    }

}
