using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonPlay : MonoBehaviour
{
    private static string gamePath = "Prefabs/Game/Game";
    private static string UIPath = "Prefabs/UI/UI";
    public static void initGameManager(){
        GameObject ui = Resources.Load(UIPath, typeof(GameObject)) as GameObject;
        ui = Instantiate(ui);
        ui.name = "UI";

        GameObject gameManager = Resources.Load(gamePath, typeof(GameObject)) as GameObject;
        gameManager = Instantiate(gameManager);
        gameManager.name = "Game";

        DontDestroyOnLoad(gameManager);
        DontDestroyOnLoad(ui);

        SceneManager.LoadScene("Floor");
    }

    public void generateDonjon(){
        initGameManager();
        Tower.generateNextFloor();
    }

    void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonPlay").TEXT;
    }
}
