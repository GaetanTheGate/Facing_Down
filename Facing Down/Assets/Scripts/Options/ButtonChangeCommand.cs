using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeCommand : MonoBehaviour
{   
    private bool canChange = false;

    private string stringKeyCode;
    public void changeCommand(){
        canChange = true;
    }

    void OnGUI(){
        print("canChange " + canChange);
        if(canChange){
            if(Input.anyKeyDown){
                if(Event.current.isMouse){
                    stringKeyCode = "Mouse" + Event.current.button;
                    print(stringKeyCode);
                    canChange = false;
                    
                }
                    
                else if(Event.current.isKey){
                    stringKeyCode = Event.current.keyCode.ToString();
                    print(stringKeyCode);
                    canChange = false;
                }

                GetComponentInChildren<Text>().text = stringKeyCode;
                
            }
                
        }
    }
    
}
