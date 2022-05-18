using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initTextActionBulletTime : MonoBehaviour
{
    void Start(){
        GetComponent<Text>().text = Localization.GetUIString("textActionBulletTime").TEXT;
    }
}
