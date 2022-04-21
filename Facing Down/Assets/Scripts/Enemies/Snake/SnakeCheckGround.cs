using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCheckGround : MonoBehaviour
{
    private string functionName = "childTriggerExitGround";

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Terrain")) gameObject.SendMessageUpwards(functionName);
    }
}
