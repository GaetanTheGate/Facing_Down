using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class initTextOptionMasterVolume : MonoBehaviour
{
    void Start(){
        GetComponent<Text>().text = Localization.GetUIString("textOptionMasterVolume").TEXT;
    }
}
