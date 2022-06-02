using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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

    public bool checkIfController(){
        if (Input.GetKey(KeyCode.JoystickButton0))
            return true;
        else if (Input.GetKey(KeyCode.JoystickButton1))
            return true;
        else if (Input.GetKey(KeyCode.JoystickButton2))
            return true;
        else if (Input.GetKey(KeyCode.JoystickButton3))
            return true;
        else if (Input.GetKey(KeyCode.JoystickButton4))
            return true;
        else if (Input.GetKey(KeyCode.JoystickButton5))
            return true;
        else if (Input.GetKey(KeyCode.JoystickButton6))
            return true;
        else if (Input.GetKey(KeyCode.JoystickButton7))
            return true;
        else if (Input.GetKey(KeyCode.JoystickButton8))
            return true;
        else if (Input.GetKey(KeyCode.JoystickButton9))
            return true;
        else if (Input.GetKey(KeyCode.JoystickButton10))
            return true;
        else if (Input.GetAxis("Button LT") > 0)
            return true;
        else if (Input.GetAxis("Button RT") > 0)
            return true;
        else if (Input.GetAxis("RightJoystickHorizontal") != 0)
            return true;
        else if (Input.GetAxis("RightJoystickVertical") != 0)
            return true;
        else if (Input.GetAxis("DiresctionalPadHorizontal") != 0)
            return true;
        else if (Input.GetAxis("DirectionalPadVertical") != 0)
            return true;
        else if (Input.GetAxis("LeftJoystickHorizontal") != 0)
            return true;
        else if (Input.GetAxis("LeftJoystickVertical") != 0)
            return true;
        else
            return false;
    }

    public bool checkIfKeyBoard(){
        if (Input.GetAxis("Mouse X") != 0)
            return true;
        else if (Input.GetAxis("Mouse Y") != 0)
            return true;
        else
            return false;
    } 
}
