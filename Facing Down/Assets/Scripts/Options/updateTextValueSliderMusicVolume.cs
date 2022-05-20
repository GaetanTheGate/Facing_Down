using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateTextValueSliderMusicVolume : MonoBehaviour
{
    public void updateMusicVolumeValue(){
        transform.Find("MusicVolumeValueText").GetComponent<Text>().text = ((GetComponent<Slider>().value + 80)*1.25f).ToString();
        MenuManager.audioMixer.SetFloat("musicVolume",GetComponent<Slider>().value);
    }
}
