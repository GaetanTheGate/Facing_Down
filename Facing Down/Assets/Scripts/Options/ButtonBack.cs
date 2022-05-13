using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBack : MonoBehaviour
{
    public void back(){
        MenuManager.gameObjectActions.SetActive(true);
        MenuManager.gameObjectOptions.SetActive(false);
    }
}
