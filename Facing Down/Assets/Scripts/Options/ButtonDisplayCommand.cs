using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonDisplayCommand : MonoBehaviour
{

    public static GameObject contentDisplayCommands;
    public static GameObject scrollRectContentDisplayCommandKeyBoard;
    public static GameObject scrollRectContentDisplayCommandController;
    public void displayCommand(){
        ButtonBack.gameObjectsToDisable.Add(contentDisplayCommands);
        MenuManager.gameObjectOptions.GetComponent<InfoSelectButton>().selectButton = gameObject;
        ButtonBack.gameObjectsToEnable.Add(MenuManager.gameObjectOptions);

        contentDisplayCommands.SetActive(true);
        ControllerManager.currentControl.SetActive(true);
        
        ButtonApply.onDisplayCommands = true;

        if(ToggleSelectableObject.onController)
            EventSystem.current.SetSelectedGameObject(GameObject.Find("ButtonRightArrow"));

    }

    void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonDisplayCommand").TEXT;
    }
}
