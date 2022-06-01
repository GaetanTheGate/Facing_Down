using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBack : MonoBehaviour
{
    public static List<GameObject> gameObjectsToEnable = new List<GameObject>();
    public static List<GameObject> gameObjectsToDisable = new List<GameObject>();

    public void back(){
        if(gameObjectsToEnable.Count == 0)
            return;
        
        gameObjectsToEnable[gameObjectsToEnable.Count -1].SetActive(true);
        if(ToggleSelectableObject.onController)
            EventSystem.current.SetSelectedGameObject(gameObjectsToEnable[gameObjectsToEnable.Count -1].GetComponent<InfoSelectButton>().selectButton);
        gameObjectsToDisable[gameObjectsToDisable.Count - 1].SetActive(false);

        if(gameObjectsToDisable[gameObjectsToDisable.Count - 1] == ButtonAdjustVolume.contentVolume)
            ButtonApply.onContentVolume = false;
        
        if(gameObjectsToDisable[gameObjectsToDisable.Count - 1] == ButtonDisplayCommand.contentDisplayCommands)
            ButtonApply.onDisplayCommands = false;
        
        gameObjectsToDisable.RemoveAt(gameObjectsToDisable.Count - 1);
        gameObjectsToEnable.RemoveAt(gameObjectsToEnable.Count -1);
    }

    void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonBack").TEXT;
    }
}
