using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneReset : MonoBehaviour
{
    public static void destroy(){
        UI.UnlockCursor();
        Destroy(GameObject.Find("UI"));
        Destroy(GameObject.Find("Game"));
    }
}
