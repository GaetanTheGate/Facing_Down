using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UI.UnlockCursor();
        Destroy(GameObject.Find("UI"));
        Destroy(GameObject.Find("Game"));
    }


}