using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    public GameObject mainCamera = GameObject.Find("Main Camera");

    void Start(){
        gameObject.transform.position = Game.currentRoom.transform.position;
    }

    public static void move(Vector3 fromPosition, Vector3 toPosition){
        
    }
}
