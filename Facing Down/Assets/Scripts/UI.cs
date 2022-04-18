using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static InventoryDisplay inventoryDisplay;

    private void Start() {
        inventoryDisplay = gameObject.GetComponent<InventoryDisplay>();
        if (inventoryDisplay == null)
            inventoryDisplay = gameObject.AddComponent<InventoryDisplay>();
    }
}
