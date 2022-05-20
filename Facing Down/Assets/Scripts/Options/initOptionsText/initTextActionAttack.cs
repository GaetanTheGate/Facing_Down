using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initTextActionAttack : MonoBehaviour
{
    void Start(){
        GetComponent<Text>().text = Localization.GetUIString("textActionAttack").TEXT;
    }
}
