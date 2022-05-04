using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static InventoryDisplay inventoryDisplay;
    public static HealthBar healthBar;
    public static Console console;

    private void Start() {
        inventoryDisplay = gameObject.GetComponent<InventoryDisplay>();
        if (inventoryDisplay == null)
            inventoryDisplay = gameObject.AddComponent<InventoryDisplay>();
        healthBar = gameObject.GetComponentInChildren<HealthBar>();
        console = gameObject.GetComponentInChildren<Console>();
    }
}
