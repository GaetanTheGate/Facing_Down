using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static GameObject gameObjectActions;
    public static GameObject gameObjectOptions;

    void Awake(){
        Localization.Init();

        gameObjectActions = GameObject.Find("Actions");
        gameObjectOptions = GameObject.Find("Options");

        ButtonDisplayCommand.contentDisplayCommand = GameObject.Find("ContentDisplayCommand");
        ButtonOptions.buttonApply = GameObject.Find("ButtonApply");
        
        gameObjectActions.SetActive(true);
        gameObjectOptions.SetActive(false);
        ButtonDisplayCommand.contentDisplayCommand.SetActive(false);

        applyOptions();
    }

    public static void applyOptions(){
        
    }
}
