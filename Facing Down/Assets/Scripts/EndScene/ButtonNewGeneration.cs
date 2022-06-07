using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Pathfinding;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNewGeneration : MonoBehaviour
{
    public void newGeneration(){
        Tower.nbFloor = 3;
        ButtonPlay.generateDonjon();
    }

    public void Start(){
        GetComponentInChildren<Text>().text = Localization.GetUIString("buttonNewGeneration").TEXT;
    }
}
