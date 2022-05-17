using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static GameObject gameObjectActions;
    public static GameObject gameObjectOptions;

    void Start(){

        gameObjectActions = GameObject.Find("Actions");
        gameObjectOptions = GameObject.Find("Options");

        ButtonDisplayCommand.contentDisplayCommand = GameObject.Find("ContentDisplayCommand");
        
        gameObjectActions.SetActive(true);
        gameObjectOptions.SetActive(false);
        ButtonDisplayCommand.contentDisplayCommand.SetActive(false);

        applyOptions();
    }

    public static void applyOptions(){
        
    }
}
