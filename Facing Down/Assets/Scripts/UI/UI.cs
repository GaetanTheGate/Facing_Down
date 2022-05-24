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

    public static MapDisplay map;

    private void Start() {
        inventoryDisplay = gameObject.GetComponentInChildren<InventoryDisplay>();
        healthBar = gameObject.GetComponentInChildren<HealthBar>();
        specialBar = gameObject.GetComponentInChildren<SpecialBar>();
        dashBar = gameObject.GetComponentInChildren<DashBar>();
        console = gameObject.GetComponentInChildren<Console>();
        map = gameObject.GetComponentInChildren<MapDisplay>();
        itemPreview = gameObject.GetComponentInChildren<ItemPreview>();

        inventoryDisplay.Init();
        inventoryDisplay.Disable();
        map.Init();
        map.Disable();

        itemPreview.Init();
        itemPreview.gameObject.SetActive(false);
    }

    /// <summary>
    /// Enables/Disables UI elements depending on user input
    /// </summary>
	private void OnGUI() {
		if (Event.current.type == EventType.KeyDown) {
            if (console.IsToggled()) {
                if (Event.current.keyCode == Options.Get().dicoCommandsKeyBoard["closeUI"] || Event.current.keyCode == Options.Get().dicoCommandsController["closeUI"] ) {
                    console.Toggle();
                    
                }
            }
            else if (inventoryDisplay.IsEnabled()) {
                if (Event.current.keyCode == Options.Get().dicoCommandsKeyBoard["closeUI"] || Event.current.keyCode == Options.Get().dicoCommandsController["closeUI"]) {
                    inventoryDisplay.Disable();
                    map.Disable();
                    LockCursor();
                    Game.time.SetGameSpeedInstant(1);
				}
            }
            else {
                if (Event.current.keyCode == Options.Get().dicoCommandsKeyBoard["openConsole"] || Event.current.keyCode == Options.Get().dicoCommandsController["openConsole"] ) {
                    console.Toggle();
                }
                else if (Event.current.keyCode == Options.Get().dicoCommandsKeyBoard["openInventoryMap"] || Event.current.keyCode == Options.Get().dicoCommandsController["openInventoryMap"] ){
                    inventoryDisplay.Enable();
                    map.Enable();
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
