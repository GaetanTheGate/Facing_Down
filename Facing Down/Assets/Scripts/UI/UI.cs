using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static InventoryDisplay inventoryDisplay;
    public static HealthBar healthBar;
    public static SpecialBar specialBar;
    public static DashBar dashBar;
    public static Console console;
    public static ItemPreview itemPreview;

    public static GameObject map;

    private void Start() {
        inventoryDisplay = gameObject.GetComponentInChildren<InventoryDisplay>();
        healthBar = gameObject.GetComponentInChildren<HealthBar>();
        specialBar = gameObject.GetComponentInChildren<SpecialBar>();
        dashBar = gameObject.GetComponentInChildren<DashBar>();
        console = gameObject.GetComponentInChildren<Console>();
        map = transform.Find("Map").gameObject;
        itemPreview = gameObject.GetComponentInChildren<ItemPreview>();

        inventoryDisplay.init();
        inventoryDisplay.Disable();
        map.SetActive(false);

        itemPreview.gameObject.SetActive(false);
    }

    /// <summary>
    /// Enables/Disables 
    /// </summary>
	private void OnGUI() {
		if (Event.current.type == EventType.KeyDown) {
            if (console.IsToggled()) {
                if (Event.current.keyCode == KeyCode.Escape) {
                    console.Toggle();
                }
            }
            else if (inventoryDisplay.IsEnabled()) {
                if (Event.current.keyCode == KeyCode.Escape) {
                    inventoryDisplay.Disable();
                    map.SetActive(false);
                    LockCursor();
                    Game.time.SetGameSpeedInstant(1);
				}
            }
            else {
                if (Event.current.keyCode == KeyCode.C) {
                    console.Toggle();
                }
                else if (Event.current.keyCode == KeyCode.E) {
                    inventoryDisplay.Enable();
                    map.SetActive(true);
                    UnlockCursor();
                    Game.time.SetGameSpeedInstant(0);
                }
            }
		}
	}

    private void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

    private void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
