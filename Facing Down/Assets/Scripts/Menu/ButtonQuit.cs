using UnityEngine;
using UnityEngine.UI;

public class ButtonQuit : MonoBehaviour
{
    public void quit(){
        Application.Quit();
    }

    void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonQuit").TEXT;
    }
}
