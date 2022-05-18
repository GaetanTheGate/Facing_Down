using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAdjustVolume : MonoBehaviour
{
    public static GameObject contentVolume;
    public void adjustVolume(){
        ButtonBack.gameObjectsToDisable.Add(contentVolume);
        ButtonBack.gameObjectsToEnable.Add(MenuManager.gameObjectOptions);

        contentVolume.SetActive(true);
        ButtonApply.onContentVolume = true;

    }

    void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonAdjustVolume").TEXT;
    }
}
