using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static GameObject gameObjectActions;
    public static GameObject gameObjectOptions;

    public static Options options;

    public static string pathOptions = "Assets/Resources/Json/Options/Options.json";

    void Start(){
        gameObjectActions = GameObject.Find("Actions");
        gameObjectOptions = GameObject.Find("Options");

        ButtonDisplayCommand.contentDisplayCommand = GameObject.Find("ContentDisplayCommand");
        
        gameObjectActions.SetActive(true);
        gameObjectOptions.SetActive(false);
        ButtonDisplayCommand.contentDisplayCommand.SetActive(false);

        options = JsonUtility.FromJson<Options>(File.ReadAllText(pathOptions));
        applyOptions();
    }

    public static void applyOptions(){
        
    }
}
