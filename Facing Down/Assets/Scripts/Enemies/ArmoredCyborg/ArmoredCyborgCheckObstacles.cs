using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCyborgCheckObstacles : MonoBehaviour
{
    public bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Terrain"))) isColliding = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Terrain"))) isColliding = false;
    }
}
