using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public static int nbFloor = 3;

    

    public static void generateFloor(){
        if(nbFloor > 0){
            Floor.resetVar();
            Floor.generateFloor();
            nbFloor -= 1;
        }
    }

    public static IEnumerator changeFloor(){
        Floor.destroyFloor();
        yield return new WaitForSeconds(1);
        Tower.generateFloor();
        UI.map.Init();
    }

}
