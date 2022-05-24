using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initTextOptionController : MonoBehaviour
{
    void Start(){
        GetComponent<Text>().text = Localization.GetUIString("textOptionCommandsKeyBoard").TEXT;
    }
}
