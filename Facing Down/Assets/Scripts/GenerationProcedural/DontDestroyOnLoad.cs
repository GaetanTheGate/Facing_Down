using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    public List<Object> objects = new List<Object>();
    void Awake(){
        foreach(Object o in objects){
            DontDestroyOnLoad(o);
        }
    }
}
