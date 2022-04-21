using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ball_bouncing : MonoBehaviour
{

    private int speed = 7;
    private int hightJump = 4;
    private Vector2 initPos;

    void Start(){
        initPos = gameObject.transform.position;
    }
    void Update(){
        if (Input.GetKey(KeyCode.D)){
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.Q)){
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        if(Input.GetKey(KeyCode.Space)){
            transform.Translate(Vector2.up * Time.deltaTime * speed*2);
        }

        if (Input.GetKeyDown(KeyCode.C)){
            print("position : ");
            print("x : " + gameObject.transform.position.x);
            print("y : " + gameObject.transform.position.y);
            
        }

    }

}
