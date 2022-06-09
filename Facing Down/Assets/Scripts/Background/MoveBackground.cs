using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class MoveBackground : MonoBehaviour
{
    public Vector3 offset = new Vector3(0,0,10);
    public float intensity = 1.05f;
    public Vector3 center = new Vector3(0, -128, 0);

    void Start(){
        Vector3 newPos = Game.player.gameCamera.transform.position + offset;
        newPos = (-center + newPos) / intensity;
        newPos = newPos + center;
        gameObject.transform.position = newPos;
    }

    void Update()
    {
        Vector3 newPos = Game.player.gameCamera.transform.position + offset;
        newPos = (-center + newPos) / intensity;
        newPos = newPos + center;
        gameObject.transform.position = newPos;
    }

}
