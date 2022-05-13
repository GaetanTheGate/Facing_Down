using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateTextValueSliderVolume : MonoBehaviour
{
    public void updateValue(){
        transform.Find("VolumeValueText").GetComponent<Text>().text = GetComponent<Slider>().value.ToString();
    }
}
