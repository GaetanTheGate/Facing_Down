using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ball_bouncing : MonoBehaviour
{

    private int speed = 7;
    private int hightJump = 4;
    private Vector2 initPos;
    private bool canJump = true;

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
        if(Input.GetKeyDown(KeyCode.Space) && canJump){
            canJump = false;
            float initY = transform.position.y;
            while(transform.position.y < initY + hightJump){
                transform.Translate(Vector2.up * Time.deltaTime * speed);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision2D){
        canJump = true;
    }
}
