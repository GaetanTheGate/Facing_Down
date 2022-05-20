using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    Transform playerTransform;
    bool isFlipped = false;

    //Start is called before the first frame update
    void Start()
    {
        playerTransform = Game.player.self.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerTransform.position.x < transform.position.x && !isFlipped)
        {
            isFlipped = true;
            gameObject.transform.localScale = new Vector2(-(Mathf.Abs(gameObject.transform.localScale.x)), gameObject.transform.localScale.y);
        }
        else if (playerTransform.position.x >= transform.position.x && isFlipped)
        {
            isFlipped = false;
            gameObject.transform.localScale = new Vector2(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y);
        }
        //transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, Vector2.Angle(playerPosition - (Vector2)transform.position, gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left));
    }
}
