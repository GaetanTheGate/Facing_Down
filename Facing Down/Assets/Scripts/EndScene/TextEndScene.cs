using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextEndScene : MonoBehaviour
{
    public static string text = "";

    public void Start(){
        GetComponent<Text>().text = text;
    }
}
