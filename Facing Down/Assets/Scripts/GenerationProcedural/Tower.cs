using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public static int nbFloor = 3;

    

    public static void generateNextFloor(){
        if(nbFloor > 0){
            Floor.resetVar();
            Floor.generateFloor();
            nbFloor -= 1;
        }

        else
            print("vous avez gagné"); 
    }

}
