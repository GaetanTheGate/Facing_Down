using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    public float movementSpeed = 100;
    public float maxDistance = 2;
    private Vector2 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (transform.localScale.x > 0.001f && (transform.position.x - startingPos.x < maxDistance))
        {
            rb.velocity = new Vector2(movementSpeed * Time.deltaTime, rb.velocity.y);
        }
        else if (transform.localScale.x < -0.001f && (Mathf.Abs(transform.position.x - startingPos.x) < maxDistance))
        {
            rb.velocity = new Vector2(-movementSpeed * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            if (transform.localScale.x > 0.001f)
                startingPos = new Vector2(startingPos.x + maxDistance, startingPos.y);
            else if (transform.localScale.x < 0.001f)
                startingPos = new Vector2(startingPos.x - maxDistance, startingPos.y);
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Terrain")) transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }
}
