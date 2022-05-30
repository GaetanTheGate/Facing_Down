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
            if(Input.GetAxis("Button LT") > 0){
                GetComponentInChildren<Text>().text = "Button LT";
                canChange = false;
            }
                
            else if(Input.GetAxis("Button RT") > 0){
                GetComponentInChildren<Text>().text = "Button RT";
                canChange = false;
            }
                
            else {
                GetComponentInChildren<Text>().text = "";
                foreach(KeyCode kc in Enum.GetValues(typeof(KeyCode))){
                    if(Input.GetKeyDown(kc)){
                        string text ;
                        if(kc.ToString().Contains("JoystickButton"))
                            text = keycodeControllerToSTring(kc);
                        else
                            text = kc.ToString();
                        GetComponentInChildren<Text>().text = text;
                        canChange = false;
                        break;
                    }
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
                bouton = "Bouton LB";
                break;
            case KeyCode.JoystickButton5:
                bouton = "Bouton RB";
                break;
            case KeyCode.JoystickButton6:
                bouton = "Bouton start";
                break;
            case KeyCode.JoystickButton7:
                bouton = "Bouton select";
                break;
            case KeyCode.JoystickButton8:
                bouton = "Bouton xBox";
                break;
            case KeyCode.JoystickButton9:
                bouton = "Bouton joystick gauche";
                break;
            case KeyCode.JoystickButton10:
                bouton = "Bouton joystick droit";
                break;
            case KeyCode.JoystickButton11:
                bouton = "Button LT";
                break;
            case KeyCode.JoystickButton12:
                bouton = "Button RT";
                break;
            default :
                bouton = kc.ToString();
                break;
        }

        return bouton;
    }

    public static KeyCode stringToKeyCode(string text){
        KeyCode kc = KeyCode.None;
        switch(text){
            case "Bouton A":
                kc = KeyCode.JoystickButton0;
                break;
            case "Bouton B":
                kc = KeyCode.JoystickButton1;
                break;
            case "Bouton X":
                kc = KeyCode.JoystickButton2;
                break;
            case "Bouton Y":
                kc = KeyCode.JoystickButton3;
                break;
            case "Bouton LB":
                kc = KeyCode.JoystickButton4;
                break;
            case "Bouton RB":
                kc = KeyCode.JoystickButton5;
                break;
            case "Bouton start":
                kc = KeyCode.JoystickButton6;
                break;
            case "Bouton select":
                kc = KeyCode.JoystickButton7;
                break;
            case "Bouton xBox":
                kc = KeyCode.JoystickButton8;
                break;
            case "Bouton joystick gauche":
                kc = KeyCode.JoystickButton9;
                break;
            case "Bouton joystick droit":
                kc = KeyCode.JoystickButton10;
                break;
            case "Button LT":
                kc = KeyCode.JoystickButton11;
                break;
            case "Button RT":
                kc = KeyCode.JoystickButton12;
                break;
            default :
                kc = KeyCode.None;
                break;
        }

        return kc;
    }


    
}
