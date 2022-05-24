using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCyborgCheckGround : MonoBehaviour
{
    private ArmoredCyborgMovement movementScript;

    private void Start()
    {
        movementScript = transform.parent.GetComponent<ArmoredCyborgMovement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Terrain"))) movementScript.childTriggerExitGround();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Terrain"))) movementScript.childTriggerEnterGround();
    }
}
