using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Luminosity.IO;
using UnityEngine.PlayerLoop;

public class ToggleSelectableObject : MonoBehaviour
{
    public static GameObject lastSelectableObject;
    public static bool onKeyBoard = true;
    public static bool onController = false;

    public UnityEvent onBack;

    void Update(){
        if(onController)
            lastSelectableObject = EventSystem.current.currentSelectedGameObject;

        if(onKeyBoard && !onController && checkIfController()){
            EventSystem.current.SetSelectedGameObject(lastSelectableObject);
            onController = true;
            onKeyBoard = false;
        }
            
        else if(onController && !onKeyBoard && checkIfKeyBoard()){
            EventSystem.current.SetSelectedGameObject(null);
            onKeyBoard = true;
            onController = false;
        }

        if(Input.GetButtonDown("Cancel") && !ButtonChangeCommand.canChange)
            onBack.Invoke();
                    
    }

    public bool checkIfController()
    {
        return InputManager.GetControlScheme("Player_Controller").AnyInput;
    }

    public bool checkIfKeyBoard()
    {
        return InputManager.GetControlScheme("Player_KeyBoard").AnyInput;
    } 
}
