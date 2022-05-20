using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateTextValueSliderSoundVolume : MonoBehaviour
{
    public void updateSoundVolumeValue(){
        transform.Find("SoundVolumeValueText").GetComponent<Text>().text = ((GetComponent<Slider>().value + 80)*1.25f).ToString();
        MenuManager.audioMixer.SetFloat("soundVolume",GetComponent<Slider>().value);

    }
}
