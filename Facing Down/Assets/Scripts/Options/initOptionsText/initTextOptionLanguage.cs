using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initTextOptionLanguage : MonoBehaviour
{
    void Start(){
        GetComponent<Text>().text = Localization.GetUIString("textOptionLanguage").TEXT;
    }
}
