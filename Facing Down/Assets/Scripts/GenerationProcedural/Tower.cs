using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour
{
    public static int nbFloor = 3;

    

    public static void generateFloor(){
        if(nbFloor > 0){
            Floor.resetVar();
            Floor.generateFloor();
            nbFloor -= 1;
        }

        else{
            TextEndScene.text = Localization.GetUIString("textEndSceneWin").TEXT;
            EndSceneReset.destroy();
            SceneManager.LoadScene("EndScene");
        }
    }

    public static IEnumerator changeFloor(){
        Game.player.gameCamera.enabled = false;
        Floor.destroyFloor();
        yield return new WaitForSeconds(1);
        Game.player.gameCamera.enabled = true;
        Tower.generateFloor();
        UI.map.Init();
    }

}
