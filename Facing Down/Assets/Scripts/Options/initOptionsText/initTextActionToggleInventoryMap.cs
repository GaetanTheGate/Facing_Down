using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initTextActionToggleInventoryMap : MonoBehaviour
{
    void Start(){
        GetComponent<Text>().text = Localization.GetUIString("textActionToggleInventoryMap").TEXT;
    }
}
