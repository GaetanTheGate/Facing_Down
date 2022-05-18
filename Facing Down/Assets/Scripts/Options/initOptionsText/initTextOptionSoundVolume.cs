using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initTextOptionSoundVolume : MonoBehaviour
{
    void Start(){
        GetComponent<Text>().text = Localization.GetUIString("textOptionSoundVolume").TEXT;
    }
}
