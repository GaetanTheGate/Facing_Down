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

        foreach(KeyBinding keyBinding in MenuManager.options.commands){
            foreach(Transform child in contentDisplayCommand.transform){
                GameObject action = child.Find("Action").gameObject;
                if(keyBinding.action == action.GetComponent<InfoAction>().idAction)
                    child.Find("KeyBinding").transform.Find("TextKey").GetComponent<Text>().text = keyBinding.key.ToString();
            }
        }

    }
}
