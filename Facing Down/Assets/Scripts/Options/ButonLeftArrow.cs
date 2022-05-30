using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButonLeftArrow : MonoBehaviour
{
    public void goLeft(){

        ControllerManager.currentControl.SetActive(false);
        if(ControllerManager.typeController.IndexOf(ControllerManager.currentControl) - 1 < 0)
            ControllerManager.currentControl = ControllerManager.typeController[ControllerManager.typeController.Count - 1];
        else
            ControllerManager.currentControl = ControllerManager.typeController[ControllerManager.typeController.IndexOf(ControllerManager.currentControl) - 1];

        GameObject.Find("TextController").GetComponent<Text>().text = Localization.GetUIString(ControllerManager.currentControl.GetComponent<InfoContentDisplayCommand>().idDisplayCommand).TEXT;

        ControllerManager.currentControl.SetActive(true);
    }
}
