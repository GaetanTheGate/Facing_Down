using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonChangeCommand : MonoBehaviour
{   
    private bool canChange = false;

    public void changeCommand(){
        canChange = true;
    }

    void Update(){
        if(canChange){
            GetComponentInChildren<Text>().text = "";
            foreach(KeyCode kc in Enum.GetValues(typeof(KeyCode))){
                if(Input.GetKeyDown(kc)){
                    GetComponentInChildren<Text>().text = kc.ToString();
                    canChange = false;
                    break;
                }
            }
                
        }
    }

    void Start(){
        transform.Find("ButtonChangeCommand").GetComponentInChildren<Text>().text = Localization.GetUIString("buttonChangeCommand").TEXT;
    }
    
}
