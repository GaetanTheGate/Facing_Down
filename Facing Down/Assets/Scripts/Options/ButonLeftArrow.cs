using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButonLeftArrow : MonoBehaviour
{
    public void goLeft(){

        ControlerManager.currentControl.SetActive(false);
        if(ControlerManager.typeControler.IndexOf(ControlerManager.currentControl) - 1 < 0)
            ControlerManager.currentControl = ControlerManager.typeControler[ControlerManager.typeControler.Count - 1];
        else
            ControlerManager.currentControl = ControlerManager.typeControler[ControlerManager.typeControler.IndexOf(ControlerManager.currentControl) - 1];

        GameObject.Find("TextControler").GetComponent<Text>().text = Localization.GetUIString(ControlerManager.currentControl.GetComponent<InfoContentDisplayCommand>().idDisplayCommand).TEXT;

        ControlerManager.currentControl.SetActive(true);
    }
}
