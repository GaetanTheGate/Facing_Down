using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Luminosity.IO;
using System;

public class ButtonChangeCommand : MonoBehaviour
{   
    public static bool canChange = false;

    public void changeCommand(){
        canChange = true;
        GetComponentInChildren<Text>().text = "";
        StartCoroutine(waitForKey());
    }


    //https://forum.unity.com/threads/waiting-for-input-in-a-custom-function.474387/
    private IEnumerator waitForKey()
    {
        bool done = false;
        while(!done){
            KeyValuePair<bool, string> result = checkIfAxesIsTrigger();
            if(result.Key){
                GetComponentInChildren<Text>().text = result.Value;
                done = true;
                canChange = false;
            }
                
            else{
                foreach(KeyCode kc in Enum.GetValues(typeof(KeyCode))){
                    if(Input.GetKeyDown(kc)){
                        string text ;
                        if(kc.ToString().Contains("JoystickButton"))
                            text = keycodeControllerToSTring(kc);
                        else
                            text = kc.ToString();
                        GetComponentInChildren<Text>().text = text;
                        done = true;
                        canChange = false;
                        break;
                    }
                }
            }
            yield return null;
        }
        
    }

    public void submitUp(){
        StartCoroutine(waitForSubmitUp());
    }

    private IEnumerator waitForSubmitUp(){
        bool done = false;
        while(!done){
            if(Input.GetButtonUp("Submit")){
                GetComponentInChildren<Button>().onClick.Invoke();
                done = true;
            }
                
            yield return null;
        }
        
    }

    public KeyValuePair<bool, string> checkIfAxesIsTrigger(){
        KeyValuePair<bool, string> result;
        if(Input.GetAxis("joy_0_axis_2") > 0)
            result = new KeyValuePair<bool, string>(true,"Button LT");
        else if (Input.GetAxis("joy_0_axis_5") > 0)
            result = new KeyValuePair<bool, string>(true,"Button RT");
        else
            result = new KeyValuePair<bool, string>(false,"");
        return result;
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
            default :
                bouton = kc.ToString();
                break;
        }

        return bouton;
    }

    public static GamepadButton stringControllerToKeyCode(string text){
        GamepadButton button = GamepadButton.Start;
        switch(text){
            case "Bouton A":
                button = GamepadButton.ActionBottom;
                break;
            case "Bouton B":
                button = GamepadButton.ActionRight;
                break;
            case "Bouton X":
                button = GamepadButton.ActionLeft;
                break;
            case "Bouton Y":
                button = GamepadButton.ActionTop;
                break;
            case "Bouton LB":
                button = GamepadButton.LeftBumper;
                break;
            case "Bouton RB":
                button = GamepadButton.RightBumper;
                break;
            case "Bouton start":
                button = GamepadButton.Start;
                break;
            case "Bouton select":
                button = GamepadButton.Back;
                break;
            case "Bouton joystick gauche":
                button = GamepadButton.LeftStick;
                break;
            case "Bouton joystick droit":
                button = GamepadButton.RightStick;
                break;
        }

        return button;
    }


    
}
