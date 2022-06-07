using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class MoveBackground : MonoBehaviour
{
    
    void Start(){
        gameObject.transform.position = Game.player.self.transform.position;
    }

    void Update(){
        gameObject.transform.position = Game.player.self.transform.position;
    }

}
