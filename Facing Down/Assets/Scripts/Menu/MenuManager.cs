using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static GameObject gameObjectActions;
    public static GameObject gameObjectOptions;

    public static Options options;

    void Start(){
        gameObjectActions = GameObject.Find("Actions");
        gameObjectOptions = GameObject.Find("Options");
        
        gameObjectActions.SetActive(true);
        gameObjectOptions.SetActive(false);

        options = JsonUtility.FromJson<Options>(Resources.Load<TextAsset>("Json/Options/Options").text);
        applyOptions();
    }

    public static void applyOptions(){
        //TO DO
    }
}
