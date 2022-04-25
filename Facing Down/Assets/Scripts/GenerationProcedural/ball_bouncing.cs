using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ball_bouncing : MonoBehaviour
{

    private int speed = 7;
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
            transform.position = new Vector2(0,0);
        }

        if (Input.GetKeyDown(KeyCode.M)){
            GameObject.Find("Map").GetComponent<Canvas>().enabled = !GameObject.Find("Map").GetComponent<Canvas>().enabled;
        }
    }

}
