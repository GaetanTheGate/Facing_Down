using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonPlay : MonoBehaviour
{
    private static string gamePath = "Prefabs/Game/Game";
    private static string UIPath = "Prefabs/UI/UI";
    public static void initGameManager(){
        SceneManager.LoadScene("Floor");

        GameObject ui = Resources.Load(UIPath, typeof(GameObject)) as GameObject;
        ui = Instantiate(ui);
        ui.name = "UI";


        GameObject gameManager = Resources.Load(gamePath, typeof(GameObject)) as GameObject;
        gameManager = Instantiate(gameManager);
        gameManager.name = "Game";


        DontDestroyOnLoad(gameManager);
        DontDestroyOnLoad(ui);
        
    }

    public static void generateDonjon(){
        initGameManager();
        Tower.nbFloor = 3;
        Tower.generateFloor();
    }

    void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonPlay").TEXT;
    }
}
