using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using Luminosity.IO;

public class ToggleSelectableObject : MonoBehaviour
{
    public static GameObject lastSelectableObject;
    public bool onKeyBoard = true;
    public bool onController = false;

    void Update(){
        if(onController)
            lastSelectableObject = EventSystem.current.currentSelectedGameObject;

        if(onKeyBoard && !onController && checkIfController()){
            print("manette");
            EventSystem.current.SetSelectedGameObject(lastSelectableObject);
            onController = true;
            onKeyBoard = false;
        }
            
        else if(onController && !onKeyBoard && checkIfKeyBoard()){
            print("clavier");
            EventSystem.current.SetSelectedGameObject(null);
            onKeyBoard = true;
            onController = false;
        }
                    
    }

    public bool checkIfController()
    {
        //return InputManager.GetControlScheme("Player_Controller").AnyInput;
        foreach(InputAction action in InputManager.GetControlScheme("Player_Controller").Actions)
            if (action.GetButton())
                return true;
        return false;
    }

    public bool checkIfKeyBoard()
    {
        foreach (InputAction action in InputManager.GetControlScheme("Player_KeyBoard").Actions)
            if (action.GetButton())
                return true;
        return false;
    } 
}
