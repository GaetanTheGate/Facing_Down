using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonAdjustVolume : MonoBehaviour
{
    public static GameObject contentVolume;
    public void adjustVolume(){
        ButtonBack.gameObjectsToDisable.Add(contentVolume);
        MenuManager.gameObjectOptions.GetComponent<InfoSelectButton>().selectButton = gameObject;
        ButtonBack.gameObjectsToEnable.Add(MenuManager.gameObjectOptions);

        contentVolume.SetActive(true);
        ButtonApply.onContentVolume = true;

        EventSystem.current.SetSelectedGameObject(GameObject.Find("SliderMasterVolume"));

    }

    void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonAdjustVolume").TEXT;
    }
}
