using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneReset : MonoBehaviour
{
    public static void destroy(){
        UI.UnlockCursor();
        Game.time.SetGameSpeedInstant(1);
        Game.time.LateUpdate();
        Destroy(GameObject.Find("UI"));
        Destroy(GameObject.Find("Game"));
    }
}
