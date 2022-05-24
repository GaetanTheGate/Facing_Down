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

    public static string keycodeControllerToSTring(KeyCode kc){
        string bouton = "";
        switch(kc){
            case KeyCode.JoystickButton0:
                bouton = "Bouton A";
                break;
            case KeyCode.JoystickButton1:
                bouton = "Bouton B";
                break;
            case KeyCode.JoystickButton2:
                bouton = "Bouton X";
                break;
            case KeyCode.JoystickButton3:
                bouton = "Bouton Y";
                break;
            case KeyCode.JoystickButton4:
                bouton = "Bouton RB";
                break;
            case KeyCode.JoystickButton0:
                bouton = "Bouton A";
                break;
            case KeyCode.JoystickButton0:
                bouton = "Bouton A";
                break;
            case KeyCode.JoystickButton0:
                bouton = "Bouton A";
                break;
            case KeyCode.JoystickButton0:
                bouton = "Bouton A";
                break;
            case KeyCode.JoystickButton0:
                bouton = "Bouton A";
                break;
            case KeyCode.JoystickButton0:
                bouton = "Bouton A";
                break;
            case KeyCode.JoystickButton0:
                bouton = "Bouton A";
                break;
        }

        return null;
    }
    
}
