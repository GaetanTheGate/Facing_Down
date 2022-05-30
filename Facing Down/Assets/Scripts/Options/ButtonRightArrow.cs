using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonRightArrow : MonoBehaviour
{
    public void goRight(){
        ControllerManager.currentControl.SetActive(false);

        if(ControllerManager.typeController.IndexOf(ControllerManager.currentControl) + 1 > ControllerManager.typeController.Count - 1)
            ControllerManager.currentControl = ControllerManager.typeController[0];
        else
            ControllerManager.currentControl = ControllerManager.typeController[ControllerManager.typeController.IndexOf(ControllerManager.currentControl) + 1];

        
        GameObject.Find("TextController").GetComponent<Text>().text = Localization.GetUIString(ControllerManager.currentControl.GetComponent<InfoContentDisplayCommand>().idDisplayCommand).TEXT;

        ControllerManager.currentControl.SetActive(true);
    }
}