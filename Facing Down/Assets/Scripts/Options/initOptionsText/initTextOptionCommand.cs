using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initTextOptionCommand : MonoBehaviour
{
    void Start(){
        GetComponent<Text>().text = Localization.GetUIString("textOptionCommand").TEXT;
    }
}
