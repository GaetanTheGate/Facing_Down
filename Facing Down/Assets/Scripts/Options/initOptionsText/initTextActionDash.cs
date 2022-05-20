using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initTextActionDash : MonoBehaviour
{
    void Start(){
        GetComponent<Text>().text = Localization.GetUIString("textActionDash").TEXT;
    }
}
