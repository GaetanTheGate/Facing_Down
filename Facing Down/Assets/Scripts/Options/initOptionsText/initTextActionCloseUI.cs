using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initTextActionCloseUI : MonoBehaviour
{
    void Start(){
        GetComponent<Text>().text = Localization.GetUIString("textActionCloseUI").TEXT;
    }
}
