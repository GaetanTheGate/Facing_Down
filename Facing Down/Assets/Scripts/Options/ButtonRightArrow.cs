using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonRightArrow : MonoBehaviour
{
    public void goRight(){
        ControlerManager.currentControl.SetActive(false);
        if(ControlerManager.typeControler.IndexOf(ControlerManager.currentControl) + 1 > ControlerManager.typeControler.Count - 1)
            ControlerManager.currentControl = ControlerManager.typeControler[0];
        else
            ControlerManager.currentControl = ControlerManager.typeControler[ControlerManager.typeControler.IndexOf(ControlerManager.currentControl) + 1];

        GameObject.Find("TextControler").GetComponent<Text>().text = Localization.GetUIString(ControlerManager.currentControl.GetComponent<InfoContentDisplayCommand>().idDisplayCommand).TEXT;

        ControlerManager.currentControl.SetActive(true);
    }
}