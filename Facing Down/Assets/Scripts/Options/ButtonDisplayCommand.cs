using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisplayCommand : MonoBehaviour
{

    public static GameObject contentDisplayCommand;
    public void displayCommand(){
        ButtonBack.gameObjectsToDisable.Add(contentDisplayCommand);
        ButtonBack.gameObjectsToEnable.Add(MenuManager.gameObjectOptions);

        contentDisplayCommand.SetActive(true);

    }
}
